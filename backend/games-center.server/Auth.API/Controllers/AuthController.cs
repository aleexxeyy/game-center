using Auth.API.Dto;
using Auth.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService service) : Controller
{
    private readonly IAuthService _service = service;

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = await _service.RegisterAsync(model);

        return user is not null 
            ? Ok($"{user.UserName} register successfuly") 
            : BadRequest("User not registered") ;
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _service.LoginAsync(model);

        return user is not null 
            ? Ok($"{user.UserName} login successfuly") 
            : Unauthorized($"{user?.UserName} unauthorized");
    }
}