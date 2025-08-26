using Auth.Application.Dto;

namespace Auth.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllRolesAsync();
    Task CreateRoleAsync(string roleName);
    Task DeleteRoleAsync(string roleName);
    Task AssignRoleToUserAsync(string userId, string role);
    Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(string role);
    Task ConfigureRolePermissionsAsync(string role, List<string> permissions);
}