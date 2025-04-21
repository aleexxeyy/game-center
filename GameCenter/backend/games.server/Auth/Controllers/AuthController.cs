using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null)
            {
                return BadRequest("Invalid registration data.");
            }

            try
            {
                var user = await _userService.RegisterAsync(registerModel);
                return Ok(new { Message = "User registered successfully", UserName = user.UserName });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid login data.");
            }

            try
            {
                var user = await _userService.LoginAsync(loginModel);
                return Ok(new { Message = "Login successful", UserName = user.UserName });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
