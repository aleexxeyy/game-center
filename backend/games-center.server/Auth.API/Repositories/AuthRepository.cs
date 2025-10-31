using Auth.API.DataAccess;
using Auth.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Auth.API.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AuthDbContext _dbcontext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthRepository(AuthDbContext dbcontext, IPasswordHasher<User> passwordHasher)
    {
        _dbcontext = dbcontext;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<User?> GetUserById(Guid id)
        => await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetUserByUserName(string userName)
        => await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    
    public async Task<User> CreateUser(RegisterModel model)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = model.UserName
        };
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, model.Password);
        
        await _dbcontext.Users.AddAsync(newUser);
        await _dbcontext.SaveChangesAsync();

        return newUser;
    }

    public async Task<User> UpdateUserName(User user)
    {
        var existingUser = await GetUserById(user.Id);

        if (existingUser is null)
            throw new KeyNotFoundException("User not found");

        existingUser.UserName = user.UserName;
        
        _dbcontext.Users.Update(existingUser);
        await _dbcontext.SaveChangesAsync();

        return existingUser;
    }

    public async Task<User> UpdateUserPassword(User user, string newPassword)
    {
        var existingUser = await GetUserById(user.Id);

        if (existingUser is null)
            throw new KeyNotFoundException("User not found");

        existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, newPassword);

        _dbcontext.Users.Update(existingUser);
        await _dbcontext.SaveChangesAsync();

        return existingUser;
    }

    public async Task DeleteUser(User user)
    {
        var existingUser = await GetUserById(user.Id);

        if (existingUser is null)
            throw new KeyNotFoundException("User not found");

        _dbcontext.Remove(existingUser);
        await _dbcontext.SaveChangesAsync();
    }
}