using GameHub.Models;
using Microsoft.EntityFrameworkCore;

namespace GameHub.DataBaseContext
{
    public class GameHubDbContext : DbContext
    {
        public GameHubDbContext(DbContextOptions<GameHubDbContext> options) : base(options) { }

        public DbSet<GameHubs> GameHubs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GameHubDb;Username=postgres;Password=donbass24iy26");
            }
        }
    }
}
