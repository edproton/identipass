namespace Infra.Repositories;

public class ClaimsRepository(AppDbContext context) : GenericRepository<Claim>(context), IClaimsRepository
{
    public Task<Claim?> GetClaimByName(string type, CancellationToken cancellationToken)
    {
        return GetByQueryAsync(claim => claim.Type == type, cancellationToken);
    }

    public Task<Claim?> GetClaimById(Guid id, CancellationToken cancellationToken)
    {
        return GetByIdAsync(id, cancellationToken);
    }
}

public class RefreshTokensRepository(AppDbContext context) : GenericRepository<RefreshToken>(context), IRefreshTokensRepository
{
    public Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken)
    {
        return GetByQueryAsync(refreshToken => refreshToken.Token == token, cancellationToken);
    }

    public Task<RefreshToken?> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return GetByQueryAsync(refreshToken => refreshToken.UserId == userId, cancellationToken);
    }
}