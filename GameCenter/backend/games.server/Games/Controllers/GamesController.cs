using GameHub.Hubs;
using GameHub.Models;
using Games.Models;
using Games.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Games.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IHubContext<GamesHub> _gameHubContext;
        private readonly GameHubs _hub;
        private readonly IGameService _service;

        public GamesController(IHubContext<GamesHub> gameHubContext, IGameService service)
        {
            _gameHubContext = gameHubContext;
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameModel>> GetAvailableGames()
        {
            var games = _service.GetAvailableGames();
            return Ok(games);
        }

        [HttpPost("select")]
        public async Task<IActionResult> SelectGame([FromBody] string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                return BadRequest("Game ID is required");

            var creator = _hub.Creator;

            var selectedGame = await _service.SelectGameAsync(gameId, creator);
            if (selectedGame == null)
                return NotFound("Игра не найдена");

            var roomId = Guid.NewGuid().ToString();

            return Ok(new { RoomId = roomId });
        }
    }
}
