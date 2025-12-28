using Auth.API.Dto;
using Auth.API.Models;
using Auth.API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Services;

public class AuthService(IAuthRepository repository, IPasswordHasher<User> passwordHasher) : IAuthService
{
    private IAuthRepository _repository = repository;
    private IPasswordHasher<User> _passwordHasher = passwordHasher;


    public async Task<User> RegisterAsync(RegisterDto model)
    {
        var exsistingUser = await _repository.GetUserByUserName(model.UserName);

        if (exsistingUser is not null)
            throw new InvalidOperationException("User name already exists");

        var newUser = await _repository.CreateUser(model);

        return newUser;
    }

    public async Task<User> LoginAsync(LoginDto model)
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