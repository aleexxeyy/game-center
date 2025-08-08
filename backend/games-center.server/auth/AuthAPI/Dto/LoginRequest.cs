using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models
{
    public record LoginRequest(string Username, string Password);
}
