namespace Infra.Repositories;

public class RolesRepository(AppDbContext context) : GenericRepository<Role>(context), IRolesRepository
{
    public Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken)
    {
        return GetByQueryAsync(role => role.Name == name, cancellationToken);
    }

    public Task<Role?> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        return GetByIdAsync(id, cancellationToken);
    }
}