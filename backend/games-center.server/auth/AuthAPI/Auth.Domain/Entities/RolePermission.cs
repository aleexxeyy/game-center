using Microsoft.AspNet.Identity.EntityFramework;

namespace Auth.Domain.Entities;

public class RolePermission
{
    public string RoleId { get; set; } = null!;
    public Guid PermissionId { get; set; }

    public IdentityRole Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}