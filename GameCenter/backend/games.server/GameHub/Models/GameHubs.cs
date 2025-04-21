using System.ComponentModel.DataAnnotations;

namespace GameHub.Models
{
    public class GameHubs
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Creator is required")]
        public string Creator { get; set; } = string.Empty;
        public string Player1 { get; set; } = string.Empty;
        public string Player2 { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}