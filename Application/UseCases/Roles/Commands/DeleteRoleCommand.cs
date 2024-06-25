namespace Application.UseCases.Roles.Commands;

public record DeleteRoleCommand(
    Guid? Id,
    string? Name) : IRequest<Result>;

public class DeleteRoleCommandHandler(
    ILogger logger,
    IRolesRepository rolesRepository) : IRequestHandler<DeleteRoleCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue && string.IsNullOrWhiteSpace(request.Name))
        {
            return Result.Failure(RequestErrors.InvalidRequest);
        }

        var existingRole = request.Id.HasValue
            ? await rolesRepository.GetByIdAsync(request.Id.Value, cancellationToken)
            : await rolesRepository.GetRoleByName(request.Name!, cancellationToken);

        if (existingRole is null)
        {
            return Result.Failure(RoleErrors.RoleNotFound(request.Name!));
        }

        await rolesRepository.DeleteAsync(existingRole, cancellationToken);

        logger.LogInformation($"[Role] {existingRole.Id} deleted");

        return Result.Success();
    }
}