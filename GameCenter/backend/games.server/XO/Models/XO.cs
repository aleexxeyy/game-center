using XO.Convertors;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XO.Models
{
    public class TicTacToe
    {
        [Required]
        public Guid Id { get; init; }
        [JsonConverter(typeof(StringArrayJsonConverter))]
        public string[,] Board { get; set; } = new string[3, 3];
        [Required]
        public string PlayerX { get; set; } = string.Empty;
        [Required]
        public string PlayerO { get; set; } = string.Empty;
        public string CurrentPlayer { get; set; } = string.Empty;
        public string? Winner { get; set; }
        public bool IsGameOver { get; set; } = false;
    }
}
