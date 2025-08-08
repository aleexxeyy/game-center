using AuthAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models
{
    public record RegisterRequest(string Username, string Password);
}
