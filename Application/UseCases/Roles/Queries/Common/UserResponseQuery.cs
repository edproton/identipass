namespace Application.UseCases.Roles.Queries.Common;

public record RoleResponseQuery(
    Guid Id,
    string Name);

public static class UserResponseQueryExtensions
{
    public static RoleResponseQuery ToQueryResponse(this Role role) => new(role.Id, role.Name);
}