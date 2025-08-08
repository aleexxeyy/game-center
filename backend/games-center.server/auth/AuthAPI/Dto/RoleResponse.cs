namespace AuthAPI.Dto;

public record RoleResponse(string Name, List<string> Permissions);