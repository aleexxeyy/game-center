using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public record RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
    }
}
