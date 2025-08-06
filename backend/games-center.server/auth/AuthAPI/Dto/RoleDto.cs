namespace AuthAPI.Dto;

public class RoleDto
{
    public required string Name { get; init; }
    public List<string> Permissions { get; init; } = new();
}