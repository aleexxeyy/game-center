using Auth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.DataAccess;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}