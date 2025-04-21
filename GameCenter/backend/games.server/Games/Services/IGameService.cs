using Games.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Games.Services
{
    public interface IGameService
    {
        IEnumerable<GameModel> GetAvailableGames();
        Task<string> SelectGameAsync(string gameId, string creator);
    }
}
