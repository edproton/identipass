namespace Application.UseCases.Management.Commands;

public record AddUserToRoleCommand(
    Guid? UserId,
    string? Username,
    Guid? RoleId,
    string RoleName) : IRequest<Result>;

public class AddUserToRoleCommandHandler(
    ILogger logger,
    IUsersRepository usersRepository,
    IRolesRepository rolesRepository) : IRequestHandler<AddUserToRoleCommand, Result>
{
    public async Task<Result> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue && string.IsNullOrWhiteSpace(request.Username))
        {
            return Result.Failure(RequestErrors.InvalidRequest);
        }
        
        if (!request.RoleId.HasValue && string.IsNullOrWhiteSpace(request.RoleName))
        {
            return Result.Failure(RequestErrors.InvalidRequest);
        }
        
        var user = request.UserId.HasValue
            ? await usersRepository.GetByIdAsync(request.UserId.Value, cancellationToken)
            : await usersRepository.GetUserByUsername(request.Username!, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound(request.Username!));
        }
        
        var role = request.RoleId.HasValue
            ? await rolesRepository.GetByIdAsync(request.RoleId.Value, cancellationToken)
            : await rolesRepository.GetRoleByName(request.RoleName!, cancellationToken);
        
        if (role is null)
        {
            return Result.Failure(RoleErrors.RoleNotFound(request.RoleName!));
        }
        
        if (user.Roles.Any(r => r.Id == role.Id))
        {
            return Result.Failure(RoleErrors.UserAlreadyInRole(user.Username, role.Name));
        }
        
        user.Roles.Add(role);
        
        await usersRepository.UpdateAsync(user, cancellationToken);
        
        logger.LogInformation($"[Management] Added user {user.Username} to role {role.Id}");

        return Result.Success();
    }
}