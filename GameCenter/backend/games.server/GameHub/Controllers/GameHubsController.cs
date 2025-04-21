using GameHub.Services;
using GameHub.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using GameHub.Models;

namespace GameHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameHubsController : ControllerBase
    {
        private readonly IGameHubsService _gameService;
        private readonly IHubContext<GamesHub> _hubContext;

        public GameHubsController(IGameHubsService gameService, IHubContext<GamesHub> hubContext)
        {
            _gameService = gameService;
            _hubContext = hubContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGame([FromBody] string creator)
        {
            var game = await _gameService.CreateHub(creator);
            await _hubContext.Clients.All.SendAsync("GameCreated", game);

            return Ok(game);
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinGame([FromQuery] Guid gameId, [FromBody] string player)
        {
            var game = await _gameService.JoinGame(gameId, player);
            if (game == null) return NotFound("Game not found or already taken");

            await _hubContext.Clients.All.SendAsync("GameUpdated", game);
            return Ok(new {game});
        }

        [HttpGet("get-games")]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameService.GetListGames();
            return Ok(games);
        }
    }
}
