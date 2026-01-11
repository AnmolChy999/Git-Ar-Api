using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GitArApi.AuthServiceApi.Documents;
using GitArApi.AuthServiceApi.Services.Command.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace GitArApi.AuthServiceApi.Services.Command;

public class TokenService : ITokenService
{

    private readonly JwtConfiguration _jwtConfig;

    public TokenService(JwtConfiguration jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }
    public string GenerateJwtToken(User user)
    {
        if (user != null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        return null;
    }
}
