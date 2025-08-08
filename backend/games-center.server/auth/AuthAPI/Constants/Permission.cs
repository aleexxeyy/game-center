using System.ComponentModel.DataAnnotations;
using AuthAPI.Models;

namespace AuthAPI.Constants;

public class Permission
{
    [Key]
    public Guid Id { get; init; }   
    
    [Required]
    public string Name { get; set; } = null!;
    
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}