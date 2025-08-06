using AuthAPI.Data;
using AuthAPI.Dto;
using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services;

public class RoleService(
    RoleManager<IdentityRole> roleManager,
    UserManager<User> userManager,
    ApplicationDbContext dbContext) : IRoleService
{
    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        return await dbContext.Roles
            .Select(r => new RoleDto
            {
                Name = r.Name,
                Permissions = dbContext.RolePermissions
                    .Where(rp => rp.RoleId == r.Id)
                    .Select(rp => rp.Permission.Name)
                    .ToList()
            })
            .ToListAsync();
    }

    public async Task CreateRoleAsync(string roleName)
    {
        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (!result.Succeeded)
            throw new InvalidOperationException($"Role creation failed: {string.Join(", ", result.Errors)}");
    }

    public async Task DeleteRoleAsync(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null) return;
        
        await roleManager.DeleteAsync(role);
    }

    public async Task AssignRoleToUserAsync(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId) 
            ?? throw new ArgumentException("User not found");
        
        if (!await roleManager.RoleExistsAsync(role))
            throw new ArgumentException("Role does not exist");

        await userManager.AddToRoleAsync(user, role);
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(string role)
    {
        var roleEntity = await roleManager.FindByNameAsync(role) 
            ?? throw new ArgumentException("Role not found");

        return await dbContext.RolePermissions
            .Where(rp => rp.RoleId == roleEntity.Id)
            .Select(rp => new PermissionDto { Code = rp.Permission.Name })
            .ToListAsync();
    }

    public async Task ConfigureRolePermissionsAsync(string role, List<string> permissions)
    {
        var roleEntity = await roleManager.FindByNameAsync(role) 
            ?? throw new ArgumentException("Role not found");

        // Получаем текущие разрешения одним запросом
        var currentPermissions = await dbContext.RolePermissions
            .Where(rp => rp.RoleId == roleEntity.Id)
            .ToListAsync();

        // Удаляем старые разрешения
        dbContext.RolePermissions.RemoveRange(currentPermissions);

        // Добавляем новые
        var permissionsToAdd = permissions.Distinct();
        foreach (var permissionName in permissionsToAdd)
        {
            var permission = await dbContext.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName)
                ?? new Permission { Id = Guid.NewGuid(), Name = permissionName };

            dbContext.RolePermissions.Add(new RolePermission
            {
                RoleId = roleEntity.Id,
                PermissionId = permission.Id
            });
        }

        await dbContext.SaveChangesAsync();
    }
}