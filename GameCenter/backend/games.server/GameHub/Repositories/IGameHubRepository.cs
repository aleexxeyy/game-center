using GameHub.Hubs;
using GameHub.Models;

namespace GameHub.Repositories
{
    public interface IGameHubRepository
    {
        Task<List<GameHubs>> GetListHubs();
        Task<GameHubs?> GetHub(Guid id);
        Task<GameHubs> AddHub(GameHubs gameHub);
        Task<GameHubs> UpdateHub(GameHubs gameHub);
    }
}
