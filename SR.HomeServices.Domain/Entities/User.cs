namespace SR.HomeServices.Domain.Entities;

/// <summary>
/// Represents an application user
/// </summary>
public class User
{
    /// <summary>User unique identifier</summary>
    public int UserId { get; set; }

    /// <summary>User email address</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>User phone number</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>Hashed password</summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>Indicates whether user is active</summary>
    public bool IsActive { get; set; }

    /// <summary>Created date </summary>
    public DateTime CreatedDate { get; set; }
}
