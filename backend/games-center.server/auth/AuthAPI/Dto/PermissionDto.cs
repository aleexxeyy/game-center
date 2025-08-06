namespace AuthAPI.Dto;

public class PermissionDto
{
    public required string Code { get; init; }
    public string DisplayName => Code.Replace("_", " ");
    public string? Category => Code.StartsWith("CAN_VIEW") ? "View" : 
        Code.StartsWith("CAN_MANAGE") ? "Management" : null;
}