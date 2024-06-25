using System.Linq.Expressions;
using Domain.Common;

namespace Application.Repositories.Common;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity?> GetByQueryAsync(Expression<Func<TEntity, bool>> query, CancellationToken cancellationToken);
    Task<PaginatedResult<TEntity>> GetAllAsync(PaginatedQuery paginatedQuery, CancellationToken cancellationToken);
    Task<PaginatedResult<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> query, PaginatedQuery paginatedQuery, CancellationToken cancellationToken);
    Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}