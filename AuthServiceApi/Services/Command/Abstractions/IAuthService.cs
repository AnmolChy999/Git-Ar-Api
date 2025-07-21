using GitArApi.AuthServiceApi.Contracts;

namespace GitArApi.AuthServiceApi.Services.Command.Abstractions;
public interface IAuthService
{
    Task RegisterUserAsync(UserRegisterRequest request, CancellationToken cancellationToken);
    Task LoginUserAsync(UserLoginRequest request, CancellationToken cancellationToken);
}