using Domain.Common;

namespace Domain.Entities;

public class Claim(
    string type,
    string value) : AuditableEntity
{
    public string Type { get; set; } = type;

    public string Value { get; set; } = value;

    public List<User> Users { get; set; } = [];
}