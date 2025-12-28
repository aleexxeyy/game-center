using Auth.API.Dto;
using Auth.API.Models;
using Auth.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService service) : Controller
{
    private readonly IAuthService _service = service;

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = await _service.RegisterAsync(model);
        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _service.LoginAsync(model);

        return Ok(user);
    }
}