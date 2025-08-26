using Auth.Application.Dto;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Infrastructure.Data;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Services;

 public class RoleService(
  RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> roleManager,
  UserManager<User> userManager,
  ApplicationDbContext dbContext) : IRoleService
 {
     public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync()
     {
         throw new NotImplementedException();
     }

     // TODOO
     public async Task CreateRoleAsync(string roleName)
     {
         throw new NotImplementedException();
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

         await userManager.AddToRoleAsync(user.ToString(), role);
     }

     // todoo
     public async Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(string role)
     {
         throw new NotImplementedException();
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