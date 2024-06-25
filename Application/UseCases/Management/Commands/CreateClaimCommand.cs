namespace Application.UseCases.Management.Commands;

public record CreateClaimCommand(
    string Type,
    string Value) : IRequest<Result>;

public class CreateClaimCommandHandler(
    ILogger logger,
    IClaimsRepository claimsRepository) : IRequestHandler<CreateClaimCommand, Result>
{
    public async Task<Result> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var existingClaim = await claimsRepository.GetByQueryAsync(c => c.Type == request.Type && c.Value == request.Value, cancellationToken);
        if (existingClaim is not null)
        {
            return Result.Failure(ClaimErrors.ClaimAlreadyExists);
        }
        
        var claim = new Claim(request.Type, request.Value);
        await claimsRepository.AddAsync(claim, cancellationToken);
        
        logger.LogInformation("[Claims] Claim created");
        
        return Result.Success();
    }
}

public record AddClaimToUserCommand(
    Guid? UserId,
    string? Username,
    string ClaimType,
    string ClaimValue) : IRequest<Result>;

public class AddClaimToUserCommandHandler(
    ILogger logger,
    IClaimsRepository claimsRepository,
    IUsersRepository usersRepository) : IRequestHandler<AddClaimToUserCommand, Result>
{
    public async Task<Result> Handle(AddClaimToUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = request.UserId.HasValue
            ? await usersRepository.GetByIdAsync(request.UserId.Value, cancellationToken)
            : await usersRepository.GetUserByUsername(request.Username!, cancellationToken);
        
        if (existingUser is null)
        {
            return Result.Failure(UserErrors.UserNotFound(request.Username!));
        }
        
        var existingClaim = await claimsRepository.GetByQueryAsync(c => c.Type == request.ClaimType && c.Value == request.ClaimValue, cancellationToken);
        if (existingClaim is null)
        {
            return Result.Failure(ClaimErrors.ClaimNotFound(request.ClaimType, request.ClaimValue));
        }
        
        if (existingUser.Claims.Any(c => c.Type == existingClaim.Type && c.Value == existingClaim.Value))
        {
            return Result.Failure(ClaimErrors.UserAlreadyHasClaim(existingUser.Id.ToString(), existingClaim.Type, existingClaim.Value));
        }
        
        existingUser.Claims.Add(existingClaim);
        
        await usersRepository.UpdateAsync(existingUser, cancellationToken);
        
        logger.LogInformation($"[Claims] Added claim {existingClaim.Type} to user {existingUser.Username}");
        
        return Result.Success();
    }
}