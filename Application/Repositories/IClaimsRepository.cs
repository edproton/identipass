namespace Application.Repositories;

public interface IClaimsRepository : IGenericRepository<Claim>
{
    public Task<Claim?> GetClaimByName(string name, CancellationToken cancellationToken);
}