
using GitArApi.AuthServiceApi.Documents;

namespace GitArApi.AuthServiceApi.Services.Command.Abstractions;

public interface ITokenService
{
    public string GenerateJwtToken(User user);
}