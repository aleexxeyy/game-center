using AuthAPI.Constants;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Models;

public class RolePermission
{
    public string RoleId { get; set; } = null!;
    public Guid PermissionId { get; set; }
    
    public IdentityRole Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}