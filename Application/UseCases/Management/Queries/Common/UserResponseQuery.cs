namespace Application.UseCases.Management.Queries.Common;

public record Role(
    Guid Id,
    string Name);

public record Claim(
    string Type,
    string Value);

public record UserResponseQuery(
    Guid Id,
    string Email,
    Role[] Roles,
    Claim[] Claims);

public static class UserResponseQueryExtensions
{
    public static UserResponseQuery ToQueryResponse(this User user) => new(
        user.Id,
        user.Email,
        user.Roles.Select(r => new Role(r.Id, r.Name)).ToArray(),
        user.Claims.Select(c => new Claim(c.Type, c.Value)).ToArray());
}