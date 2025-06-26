using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; } = string.Empty;
    }
}
