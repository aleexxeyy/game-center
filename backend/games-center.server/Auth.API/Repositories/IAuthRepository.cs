using Auth.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Repositories;

public interface IAuthRepository
{
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByUserName(string userName);
    Task<User> CreateUser(RegisterModel model);
    Task<User> UpdateUserName(User user);
    Task<User> UpdateUserPassword(User user, string newPassword);
    Task DeleteUser(User user);
}