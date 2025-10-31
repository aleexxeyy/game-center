using Auth.API.Models;
using Auth.API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Services;

public class AuthService(IAuthRepository repository, IPasswordHasher<User> passwordHasher) : IAuthService
{
    private IAuthRepository _repository = repository;
    private IPasswordHasher<User> _passwordHasher = passwordHasher;


    public Task<User> RegisterAsync(RegisterModel model)
    {
        var exsistingUser = _repository.GetUserByUserName(model.UserName);

        if (exsistingUser is not null)
            throw new Exception("User is already user name is exist");

        var newUser = _repository.CreateUser(model);

        return newUser;
    }

    public async Task<User> LoginAsync(LoginModel model)
    {
        var user = await _repository.GetUserByUserName(model.UserName);

        if (user is null)
            throw new KeyNotFoundException("User not found");

        var passwordHash = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

        if (passwordHash == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Password is not correct");

        return user;
    }
}