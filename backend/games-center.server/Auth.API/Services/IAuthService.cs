using Auth.API.Dto;
using Auth.API.Models;

namespace Auth.API.Services;

public interface IAuthService
{
    Task<User> RegisterAsync(RegisterDto model);
    Task<User> LoginAsync(LoginDto model);
}