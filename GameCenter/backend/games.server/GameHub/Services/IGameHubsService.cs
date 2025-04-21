using GameHub.Models;

namespace GameHub.Services
{
    public interface IGameHubsService
    {
        Task<List<GameHubs>> GetListGames();
        Task<GameHubs> CreateHub(string creator);
        Task<GameHubs?> JoinGame(Guid gameId, string player);
    }
}