namespace SR.HomeServices.Domain.Entities;

/// <summary>
/// Represents a system role
/// </summary>
public class Role
{

    /// <summary>Role Identifier</summary>
    public int RoleId { get; set; }

    /// <summary>Rolename </summary>
    public string RoleName { get; set; } = string.Empty;
}
