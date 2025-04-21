using XO.Models;
using GameHub.Models;
using System.Threading.Tasks;

namespace XO.Services
{
    public interface IXOService
    {
        Task<TicTacToe> CreateGame(Guid hubId);
        Task<TicTacToe?> MakeMoveAsync(TicTacToe game, int row, int col);
        bool CheckWinner(TicTacToe game);
        Task<bool> SetWinnerAsync(TicTacToe game, string winnerSymbol);
    }
}
