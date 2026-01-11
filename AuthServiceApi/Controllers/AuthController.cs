using GitArApi.AuthServiceApi.Contracts;
using GitArApi.AuthServiceApi.Documents;
using GitArApi.AuthServiceApi.Services.Command.Abstractions;
using GitArApi.Common.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace GitArApi.AuthServiceApi.Controllers;

[ApiController]
[Route("auth/v1.0")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    private readonly HttpContextAccessor _contextAccessor;

    public AuthController
    (
        IAuthService authService,
        HttpContextAccessor contextAccessor
    )
    {
        _authService = authService;
        _contextAccessor = contextAccessor;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        await _authService.RegisterUserAsync(request, cancellationToken);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginUserAsync(request, cancellationToken);
        return Ok(token);
    }
}