using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Application.Dto;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Services;

public class AuthService(UserManager<User> userManager, IConfiguration config) : IAuthService
{
    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Username);

        if (user is null)
            return null;

        var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
            return null;

        var roles = await userManager.GetRolesAsync(user.ToString());

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT_SECRET"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}