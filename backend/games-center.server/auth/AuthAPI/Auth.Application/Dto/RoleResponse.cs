namespace Auth.Application.Dto;

public record RoleResponse(string Name, List<string> Permissions);