using AuthAPI.Constants;
using AuthAPI.Dto;

namespace AuthAPI.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllRolesAsync();
    Task CreateRoleAsync(string roleName);
    Task DeleteRoleAsync(string roleName);
    Task AssignRoleToUserAsync(string userId, string role);
    Task<IEnumerable<Permission>> GetPermissionsForRoleAsync(string role);
    Task ConfigureRolePermissionsAsync(string role, List<string> permissions);
}