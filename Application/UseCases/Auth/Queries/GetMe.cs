namespace Application.UseCases.Auth.Queries;

public record GetMeQuery(
    string Token,
    string RefreshToken) : IRequest<Result<GetMeQueryResponse>>;


public class GetMeQueryHandler(
    ILogger logger,
    ITokenService tokenService,
    IRefreshTokensRepository refreshTokensRepository,
    IUsersRepository usersRepository) : IRequestHandler<GetMeQuery, Result<GetMeQueryResponse>>
{
    public async Task<Result<GetMeQueryResponse>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var userId = tokenService.GetUserIdFromToken(request.Token);
        var user = await usersRepository.GetUserById(userId);
        if (user is null)
        {
            return Result.Failure<GetMeQueryResponse>(UserErrors.UserNotFound(userId.ToString()));
        }
        
        var refreshToken = await refreshTokensRepository.GetByToken(request.RefreshToken, cancellationToken);
        if (refreshToken is null || refreshToken.UserId != userId || refreshToken.Revoked != null)
        {
            logger.LogWarning($"[User] {userId} tried to get me with invalid refresh token {request.RefreshToken}");

            return Result.Failure<GetMeQueryResponse>(RefreshTokensErrors.InvalidRefreshToken);
        }

        return Result.Success(new GetMeQueryResponse(
            user.Id,
            user.Email,
            user.Username,
            user.Roles.Select(r => r.Name),
            user.Claims.Select(c => new GetMeQueryClaimResponse(c.Type, c.Value))));
    }
}

public record GetMeQueryClaimResponse(
    string Type,
    string Value);

public record GetMeQueryResponse(
    Guid Id,
    string Email,
    string? Username,
    IEnumerable<string> Roles,
    IEnumerable<GetMeQueryClaimResponse> Claims);
    
    
