using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return result;

            await _userManager.AddToRoleAsync(user, Roles.User);

            return result;
        }

        public async Task<string?> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null)
                return null;

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            
            // Todoo
        }
    }
}
