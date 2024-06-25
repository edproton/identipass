using Domain.Common;

namespace Domain.Entities;

public sealed class User : AuditableEntity
{
    public string? FirstName { get; set; } = string.Empty;
    
    public string? LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;

    public string? Username { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;

    public List<Role> Roles { get; set; } = [];
    
    public List<Claim> Claims { get; set; } = [];
    
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}

public sealed class Role : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
}