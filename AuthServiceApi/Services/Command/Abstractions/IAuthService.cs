using GitArApi.AuthServiceApi.Contracts;

namespace GitArApi.AuthServiceApi.Services.Command.Abstractions;
public interface IAuthService
{
    Task RegisterUserAsync(UserRegisterRequest request, CancellationToken cancellationToken);
    Task<string> LoginUserAsync(UserLoginRequest request, CancellationToken cancellationToken);
}