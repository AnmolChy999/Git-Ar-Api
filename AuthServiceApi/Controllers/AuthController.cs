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

    
    
}