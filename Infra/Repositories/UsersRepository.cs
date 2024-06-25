using Application.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UsersRepository(AppDbContext context) : GenericRepository<User>(context), IUsersRepository
{
    private readonly AppDbContext _context = context;

    public override Task<PaginatedResult<User>> GetAllAsync(PaginatedQuery paginatedQuery, CancellationToken cancellationToken)
    {
        return _context
            .Users
            .Include(u => u.Roles)
            .Include(u => u.Claims)
            .ToPaginatedResultAsync(paginatedQuery, cancellationToken);
    }

    public Task<User?> GetUserByUsername(string name, CancellationToken cancellationToken)
    {
        var user = _context.Users
            .Include(u => u.Roles)
            .Include(u => u.Claims)
            .FirstOrDefaultAsync(user => user.Username == name, cancellationToken);

        return user;
    }

    public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        var user = _context.Users
            .Include(u => u.Roles)
            .Include(u => u.Claims)
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        return user;
    }

    public Task<User?> GetUserById(Guid id)
    {
        return _context.Users
            .Include(u => u.Roles)
            .Include(u => u.Claims)
            .FirstOrDefaultAsync(user => user.Id == id);
    }
}