using Auth.Models;
using Auth.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User> RegisterAsync(RegisterModel registerModel)
        {
            var existingUser = await _usersRepository.GetByUserName(registerModel.UserName);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this username already exists.");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = registerModel.UserName
            };  

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerModel.Password);

            await _usersRepository.AddUser(newUser);
            return newUser;
        }

        public async Task<User> LoginAsync(LoginModel loginModel)
        {
            var user = await _usersRepository.GetByUserName(loginModel.UserName);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username");
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            return user;
        }
    }
}
