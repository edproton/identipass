namespace Application.Repositories;

public interface IUsersRepository : IGenericRepository<User>
{
    public Task<User?> GetUserByUsername(string name, CancellationToken cancellationToken);
    
    public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    
    Task<User?> GetUserById(Guid id);
}