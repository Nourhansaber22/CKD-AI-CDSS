using CKD_AI_CDSS.Domain.Common;
using CKD_AI_CDSS.Domain.Enums;

namespace CKD_AI_CDSS.Domain.Entites;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;

    public int FailedLoginAttempts { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}