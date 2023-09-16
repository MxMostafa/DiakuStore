using DiakuSoft.Server.Domain.Dtos.Request.Auth;
using DiakuSoft.Server.Domain.Interfaces.Service;

namespace DiakuSoft.Server.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;

    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginReqDto loginReqDto)
    {
        var result = await _authService.LoginAsync(loginReqDto);
        return Ok(result);
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> RegisterByEmail(RegisterationByEmailReqDto registerationByEmailReqDto)
    {
        var result = await _authService.RegisterByEmailAsync(registerationByEmailReqDto);
        return Ok(result);
    }

    
}
