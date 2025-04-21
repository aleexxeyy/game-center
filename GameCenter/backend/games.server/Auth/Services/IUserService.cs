using Auth.Models;

namespace Auth.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterModel registerModel);
        Task<User> LoginAsync(LoginModel loginModel);
    }
}
