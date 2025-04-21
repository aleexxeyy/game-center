using GameHub.DataBaseContext;
using GameHub.Hubs;
using GameHub.Models;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Repositories
{
    public class GameHubRepository : IGameHubRepository
    {
        private readonly GameHubDbContext _dbContext;

        public GameHubRepository(GameHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GameHubs> AddHub(GameHubs gameHub)
        {
            await _dbContext.AddAsync(gameHub);
            await _dbContext.SaveChangesAsync();
            return gameHub;
        }

        public async Task<GameHubs?> GetHub(Guid id)
        {
            return await _dbContext.GameHubs
                .AsNoTracking() 
                .FirstOrDefaultAsync(h => h.Id == id && h.Status == "second player expected");
        }

        public async Task<List<GameHubs>> GetListHubs()
        {
            return await _dbContext.GameHubs
                .AsNoTracking()
                .Where(g => g.Status == "second player expected" && g.Status == "in progress")
                .ToListAsync();
        }

        public async Task<GameHubs> UpdateHub(GameHubs gameHub)
        {
            _dbContext.GameHubs.Update(gameHub);
            await _dbContext.SaveChangesAsync();
            return gameHub;
        }
    }
}
