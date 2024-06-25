using System.Linq.Expressions;
using Application.Repositories.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Common;

public class GenericRepository<TEntity>(
    AppDbContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public Task<TEntity?> GetByQueryAsync(Expression<Func<TEntity, bool>> query, CancellationToken cancellationToken)
    {
        return context.Set<TEntity>().FirstOrDefaultAsync(query, cancellationToken);
    }

    public virtual Task<PaginatedResult<TEntity>> GetAllAsync(PaginatedQuery paginatedQuery, CancellationToken cancellationToken)
    {
        return context.Set<TEntity>()
            .ToPaginatedResultAsync(paginatedQuery, cancellationToken);
    }

    public Task<PaginatedResult<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> query,
        PaginatedQuery paginatedQuery,
        CancellationToken cancellationToken)
    {
        return context.Set<TEntity>()
            .Where(query)
            .ToPaginatedResultAsync(paginatedQuery, cancellationToken);
    }

    public async Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Update(entity);

        return context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Remove(entity);

        return context.SaveChangesAsync(cancellationToken);
    }
}