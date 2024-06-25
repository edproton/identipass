using Domain.Common;

namespace Domain.Entities;

public class RefreshToken : AuditableEntity
{
    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;

    public DateTime Created { get; set; }

    public string CreatedByIp { get; set; } = null!;

    public DateTime? Revoked { get; set; }

    public string? RevokedByIp { get; set; }

    public string? ReplacedByToken { get; set; }

    public bool IsActive => Revoked == null && !IsExpired;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}