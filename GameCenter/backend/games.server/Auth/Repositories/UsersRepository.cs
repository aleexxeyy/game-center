using Auth.DataBase;
using Auth.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Auth.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataBaseContext _context;

        public UsersRepository(DataBaseContext context)
        {   
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}