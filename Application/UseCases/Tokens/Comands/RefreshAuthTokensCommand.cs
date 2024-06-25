using Application.UseCases.RefreshTokens.Comands;

namespace Application.UseCases.Tokens.Comands;

public record RefreshAuthTokensCommand(
    string Token,
    string RefreshToken) : IRequest<Result<RefreshTokensCommandResponse>>;

public class RefreshAuthTokensCommandHandler(
    ILogger logger,
    IUsersRepository usersRepository,
    ITokenService tokenService,
    IRefreshTokensRepository refreshTokensRepository,
    ISender mediator)
    : IRequestHandler<RefreshAuthTokensCommand, Result<RefreshTokensCommandResponse>>
{
    public async Task<Result<RefreshTokensCommandResponse>> Handle(
        RefreshAuthTokensCommand request,
        CancellationToken cancellationToken)
    {
        var userIdFromToken = tokenService.GetUserIdFromToken(request.Token);

        var user = await usersRepository.GetUserById(userIdFromToken);
        if (user is null)
        {
            return Result.Failure<RefreshTokensCommandResponse>(UserErrors.UserNotFound(userIdFromToken.ToString()));
        }

        var refreshToken = await refreshTokensRepository.GetByToken(request.RefreshToken, cancellationToken);
        if (refreshToken is null || refreshToken.UserId != user.Id)
        {
            logger.LogWarning($"[User] {user.Id} tried to refresh with invalid refresh token {request.RefreshToken}");

            return Result.Failure<RefreshTokensCommandResponse>(Error.NotFound("refresh_token_not_found",
                "Refresh token not found"));
        }

        if (refreshToken.Revoked != null)
        {
            logger.LogWarning($"[User] {user.Id} tried to refresh with revoked refresh token {request.RefreshToken}");

            return Result.Failure<RefreshTokensCommandResponse>(Error.NotFound("refresh_token_revoked",
                "Refresh token already revoked"));
        }

        var revokeRefreshToken = await mediator.Send(
            new RevokeRefreshTokenCommand(user.Id, request.RefreshToken),
            cancellationToken);

        if (revokeRefreshToken.IsFailure)
        {
            return Result.Failure<RefreshTokensCommandResponse>(revokeRefreshToken.Error!);
        }
        
        var newAccessToken = tokenService.GenerateToken(user);
        var newRefreshToken = await mediator.Send(new CreateRefreshTokenCommand(user.Id), cancellationToken);

        return Result.Success(new RefreshTokensCommandResponse(newAccessToken, newRefreshToken.Value));
    }
}

public record RefreshTokensCommandResponse(
    string Token,
    string RefreshToken);