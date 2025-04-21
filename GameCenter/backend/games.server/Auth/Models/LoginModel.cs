    using System.ComponentModel.DataAnnotations;

    namespace Auth.Models
    {
        public class LoginModel
        {
            [Required(ErrorMessage = "Username is required")]
            public string UserName { get; init; }
            [Required(ErrorMessage = "Password is required")]
            public string Password { get; init; }
        }
    }
