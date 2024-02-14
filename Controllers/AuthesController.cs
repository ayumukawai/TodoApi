using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthesController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var sessionId = await _authService.Login(request.UserName, request.Password);
        if (sessionId == null) return Unauthorized();
        return Ok(new { sessionId });
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout(LogoutRequest request)
    {
        var success = await _authService.Logout(request.SessionId);
        if (!success) return BadRequest("Invalid session ID");
        return Ok("Logged out successfully");
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        var success = await _authService.Register(request.UserName, request.Password);
        if (!success) return BadRequest("Username already exists");
        return Ok("User registered successfully");
    }
}

public class LoginRequest
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LogoutRequest
{
    public string SessionId { get; set; } = null!;
}

public class RegisterRequest
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}