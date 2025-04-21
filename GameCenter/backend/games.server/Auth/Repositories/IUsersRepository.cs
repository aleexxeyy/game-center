using Auth.Models;

namespace Auth.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetByUserName(string username);
        Task<User> AddUser(User user);
    }
}
