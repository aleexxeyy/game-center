using XO.Models;
using GameHub.Hubs;
using GameHub.Models;
using GameHub.Repositories;

namespace XO.Services
{
    public class XOService : IXOService
    {
        private readonly IGameHubRepository _hubRepository;

        public XOService(IGameHubRepository hubRepository)
        {
            _hubRepository = hubRepository;
        }

        public async Task<TicTacToe> CreateGame(Guid hubId)
        {
            var gameHub = await _hubRepository.GetHub(hubId);

            if (gameHub?.Id == null)
                throw new ArgumentNullException(nameof(hubId), "Hub id is null");
            
            var game = new TicTacToe
            {
                Id = gameHub.Id,
                Board = new string[3,3],
                CurrentPlayer = gameHub.Player1,
                PlayerX = gameHub.Player1,
                PlayerO = gameHub.Player2,
            };

            return game;
        }

        public bool CheckWinner(TicTacToe game)
        {
            var board = game.Board;
            string current = game.CurrentPlayer;

            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == current && board[i, 1] == current && board[i, 2] == current)
                    return true;
                if (board[0, i] == current && board[1, i] == current && board[2, i] == current)
                    return true;
            }

            if (board[0, 0] == current && board[1, 1] == current && board[2, 2] == current)
                return true;

            if (board[0, 2] == current && board[1, 1] == current && board[2, 0] == current)
                return true;

            return false;
        }


        public async Task<TicTacToe?> MakeMoveAsync(TicTacToe game, int row, int col)
        {
            if (game.IsGameOver || !string.IsNullOrEmpty(game.Board[row, col]))
                return null;

            game.Board[row, col] = game.CurrentPlayer;

            if (CheckWinner(game))
            {
                game.Winner = game.CurrentPlayer == "X" ? game.PlayerX : game.PlayerO;
                game.IsGameOver = true;
            }
            else
            {
                game.CurrentPlayer = game.CurrentPlayer == "X" ? "O" : "X";
            }

            await UpdateGameAsync(game);
            return game;
        }


        public async Task<bool> SetWinnerAsync(TicTacToe game, string winnerSymbol)
        {
            if (game == null || string.IsNullOrEmpty(winnerSymbol))
                return false;

            game.Winner = winnerSymbol == "X" ? game.PlayerX : game.PlayerO;
            game.IsGameOver = true;
            await UpdateGameAsync(game);
            return true;
        }

        private async Task UpdateGameAsync(TicTacToe game)
        {
            var gameHub = await _hubRepository.GetHub(game.Id);
            if (gameHub == null)
            {
                throw new InvalidOperationException($"GameHub with ID {game.Id} not found.");
            }

            game.PlayerX = gameHub.Player1;
            game.PlayerO = gameHub.Player2;
            gameHub.Status = game.IsGameOver ? "finished" : "in progress";
            gameHub.Id = game.Id;

            await _hubRepository.UpdateHub(gameHub);
        }
    }
}
