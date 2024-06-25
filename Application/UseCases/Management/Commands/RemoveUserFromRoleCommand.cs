namespace Application.UseCases.Management.Commands;

public record RemoveUserFromRoleCommand(
    Guid? UserId,
    string? Username,
    Guid? RoleId,
    string RoleName) : IRequest<Result>;

public class RemoveUserFromRoleCommandHandler(
    ILogger logger,
    IUsersRepository usersRepository,
    IRolesRepository rolesRepository) : IRequestHandler<RemoveUserFromRoleCommand, Result>
{
    public async Task<Result> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
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
        
        if (!user.Roles.Contains(role))
        {
            return Result.Failure(RoleErrors.UserNotInRole(request.Username!, request.RoleName!));
        }
        
        user.Roles.Remove(role);
        
        await usersRepository.UpdateAsync(user, cancellationToken);
        
        logger.LogInformation($"[Management] Removed user {user.Username} from role {role.Id}");

        return Result.Success();
    }
}