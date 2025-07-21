using System.Security.Cryptography;
using GitArApi.AuthServiceApi.Contracts;
using GitArApi.AuthServiceApi.Documents;
using GitArApi.AuthServiceApi.Services.Command.Abstractions;
using GitArApi.Common.Mongo;
using GitArApi.Common.Constants;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MongoDB.Driver;
using System.Security.AccessControl;

public class AuthService : IAuthService
{
    private readonly IDocumentStore<User> _userStore;

    public AuthService(IDocumentStore<User> userStore)
    {
        _userStore = userStore;
    }
    public Task LoginUserAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterUserAsync(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        var existingFilter = Builders<User>.Filter.Eq(u => u.UserName, request.UserName);
        var exists = await _userStore.GetDocumentAsync(existingFilter, cancellationToken);
        if (exists != null)
        {
            throw new Exception("Username already exists, use different username");
        }
        var saltAndHash = HashPassword(request.Password);
        var user = new User()
        {
            UserName = request.UserName,
            Password = saltAndHash.hash,
            PasswordSalt = saltAndHash.salt
        };
        await _userStore.InsertAsync(user, cancellationToken);
    }

    private (string salt, string hash) HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(PasswordConstants.SaltSize);
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256,PasswordConstants.IterationCount ,PasswordConstants.KeySize));
        return (Convert.ToBase64String(salt), hash);
    }

    private bool VerifyPassword(string salt, string hash, string password)
    {
        var saltByte = Convert.FromBase64String(salt);
        var generatedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, saltByte, KeyDerivationPrf.HMACSHA256, PasswordConstants.IterationCount, PasswordConstants.KeySize));
        return hash == generatedHash;
    }
}