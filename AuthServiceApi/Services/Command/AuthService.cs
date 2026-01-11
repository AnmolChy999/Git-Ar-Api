using System.Security.Cryptography;
using GitArApi.AuthServiceApi.Contracts;
using GitArApi.AuthServiceApi.Documents;
using GitArApi.AuthServiceApi.Services.Command.Abstractions;
using GitArApi.Common.Mongo;
using GitArApi.Common.Constants;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IDocumentStore<User> _userStore;
    private readonly JwtConfiguration _configuration;

    private readonly ITokenService _tokenService;

    public AuthService(IDocumentStore<User> userStore, JwtConfiguration configuration, ITokenService tokenService)
    {
        _userStore = userStore;
        _configuration = configuration;
        _tokenService = tokenService;
    }
    public async Task<string> LoginUserAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var existingFilter = Builders<User>.Filter.Eq(u => u.UserName, request.UserName);
        var exists = await _userStore.GetDocumentAsync(existingFilter, cancellationToken);
        if (exists == null)
        {
            throw new Exception("User not found");
        }
        
            if(!VerifyPassword(exists.PasswordSalt, exists.Password, request.Password))
        {
            throw new Exception("Invalid password");
        }
        var token = _tokenService.GenerateJwtToken(exists);
        
        return token;

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

    // private string GenerateJwtToken(User user)
    // {
    //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
    //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //     var claims = new[]
    //     {
    //         new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
    //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    //     };

    //     var token = new JwtSecurityToken(
    //         issuer: _configuration.Issuer,
    //         audience: _configuration.Audience,
    //         claims: claims,
    //         expires: DateTime.Now.AddMinutes(120),
    //         signingCredentials: credentials);

    //     return new JwtSecurityTokenHandler().WriteToken(token);
    // }
}