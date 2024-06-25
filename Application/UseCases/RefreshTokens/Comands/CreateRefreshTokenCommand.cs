using System.Security.Cryptography;

namespace Application.UseCases.RefreshTokens.Comands;

public record CreateRefreshTokenCommand(
    Guid UserId) : IRequest<Result<string>>;

public class CreateRefreshTokenCommandHandler(
    ILogger logger,
    IUsersRepository usersRepository,
    IRefreshTokensRepository refreshTokensRepository) : IRequestHandler<CreateRefreshTokenCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserById(request.UserId);
        if (user is null)
        {
            return Result.Failure<string>(UserErrors.UserNotFound(request.UserId.ToString()));
        }

        var rng = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(rng);

        var expires = DateTime.UtcNow.AddDays(7);
        var refreshToken = new RefreshToken
        {
            Token = token,
            Expires = expires,
            CreatedByIp = "user_ip_address", // Placeholder Ip Address, replace with actual user Ip Address
            UserId = user.Id
        };

        await refreshTokensRepository.AddAsync(refreshToken, cancellationToken);

        logger.LogInformation($"[User] {user.Id} created refresh token {token}");

        return Result.Success(token);
    }
}