namespace Application.UseCases.Roles.Commands;

public record CreateRoleCommand(
    string Name) : IRequest<Result<Guid>>;

public class CreateRoleCommandHandler(
    ILogger logger,
    IRolesRepository rolesRepository) : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await rolesRepository.GetRoleByName(request.Name, cancellationToken);
        if (existingRole is not null)
        {
            logger.LogWarning($"[Role] {request.Name} already exists");

            return Result.Failure<Guid>(RoleErrors.RoleAlreadyExists(request.Name));
        }

        var role = new Role
        {
            Name = request.Name
        };

        await rolesRepository.AddAsync(role, cancellationToken);

        logger.LogInformation($"[Role] {role.Id} created");

        return Result.Success(role.Id);
    }
}