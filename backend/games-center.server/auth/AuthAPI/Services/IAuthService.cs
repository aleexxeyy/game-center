using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<string?> LoginAsync(LoginModel model);
    }
}
