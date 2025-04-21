using GameHub.Models;
using GameHub.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameHub.Services
{
    public class GameHubsService : IGameHubsService
    {
        private readonly IGameHubRepository _gameHubRepository;

        public GameHubsService(IGameHubRepository gameHubRepository)
        {
            _gameHubRepository = gameHubRepository;
        }

        public async Task<List<GameHubs>> GetListGames()
        {
            return await _gameHubRepository.GetListHubs();
        }

        public async Task<GameHubs> CreateHub(string creator)
        {
            var gameHub = new GameHubs
            {
                Id = Guid.NewGuid(),
                Creator = creator,
                Player1 = creator,
                CreatedAt = DateTime.UtcNow,
                Status = "second player expected"
            };

            return await _gameHubRepository.AddHub(gameHub);
        }

        public async Task<GameHubs> JoinGame(Guid gameId, string player)
        {
            var game = await _gameHubRepository.GetHub(gameId);
            if (game == null || !string.IsNullOrEmpty(game.Player2))
            {
                throw new Exception("Hub not found or second player not connected");
            }

            game.Player2 = player;
            game.Status = "in progress";

            await _gameHubRepository.UpdateHub(game);
           
            return game;
        }
    }
}
