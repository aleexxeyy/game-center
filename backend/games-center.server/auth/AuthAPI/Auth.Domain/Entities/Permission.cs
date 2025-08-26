using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities;

public class Permission
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public string Name { get; set; } = null!;

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}