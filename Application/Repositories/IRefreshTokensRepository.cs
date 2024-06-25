namespace Application.Repositories;

public interface IRefreshTokensRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken);
}