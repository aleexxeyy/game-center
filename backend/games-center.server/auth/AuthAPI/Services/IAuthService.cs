using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest request);
        Task<string?> LoginAsync(LoginRequest request);
    }
}
