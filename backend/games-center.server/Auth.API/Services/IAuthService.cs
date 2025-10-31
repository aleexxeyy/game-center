using Auth.API.Models;

namespace Auth.API.Services;

public interface IAuthService
{
    Task<User> RegisterAsync(RegisterModel model);
    Task<User> LoginAsync(LoginModel model);
}