namespace Application.UseCases.Roles.Commands;

public record UpdateRoleCommand(
    Guid? Id,
    string? Name,
    UpdateRoleFields UpdateFields) : IRequest<Result>;

public record UpdateRoleFields(
    string? Name);

public class UpdateRoleCommandHandler(
    ILogger logger,
    IRolesRepository rolesRepository) : IRequestHandler<UpdateRoleCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
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

        if (request.UpdateFields.Name is not null && request.UpdateFields.Name != existingRole.Name)
        {
            existingRole.Name = request.UpdateFields.Name;
        }

        await rolesRepository.UpdateAsync(existingRole, cancellationToken);

        logger.LogInformation($"[Role] {existingRole.Id} updated");

        return Result.Success();
    }
}