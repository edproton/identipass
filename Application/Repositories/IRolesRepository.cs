namespace Application.Repositories;

public interface IRolesRepository : IGenericRepository<Role>
{
    public Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken);
    
    public Task<Role?> GetRoleById(Guid id, CancellationToken cancellationToken);
}