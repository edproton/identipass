namespace Application.UseCases.RefreshTokens.Comands;

public record RevokeRefreshTokenCommand(
    Guid UserId,
    string RefreshToken) : IRequest<Result>;
    
public class RevokeRefreshTokenCommandHandler(
    ILogger logger,
    IRefreshTokensRepository refreshTokensRepository) : IRequestHandler<RevokeRefreshTokenCommand, Result>
{
    public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await refreshTokensRepository.GetByToken(request.RefreshToken, cancellationToken);
        if (refreshToken is null || refreshToken.UserId != request.UserId)
        {
            logger.LogWarning($"[User] {request.UserId} tried to revoke invalid refresh token {request.RefreshToken}");

            return Result.Failure(Error.NotFound("refresh_token_not_found", "Refresh token not found"));
        }
        
        if (refreshToken.Revoked != null)
        {
            logger.LogWarning($"[User] {request.UserId} tried to revoke already revoked refresh token {request.RefreshToken}");

            return Result.Failure(Error.NotFound("refresh_token_revoked", "Refresh token already revoked"));
        }
        
        refreshToken.Revoked = DateTime.UtcNow;
        
        await refreshTokensRepository.UpdateAsync(refreshToken, cancellationToken);
        
        logger.LogInformation($"[User] {request.UserId} revoked refresh token {request.RefreshToken}");

        return Result.Success();
    }
}