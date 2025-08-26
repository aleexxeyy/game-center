using Auth.Application.Dto;
using Microsoft.AspNet.Identity;

namespace Auth.Application.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}