namespace Auth.API.Models;

public class User
{
    public Guid Id { get; init; }

    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
}