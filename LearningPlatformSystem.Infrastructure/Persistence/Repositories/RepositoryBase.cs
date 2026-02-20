using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<TAggregate, TEntity, TId, TDbContext>(TDbContext context) : IRepositoryBase<TAggregate, TId> where TEntity : class where TDbContext : DbContext
{
    protected readonly TDbContext Context = context;
    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    public virtual async Task AddAsync(TAggregate aggregate, CancellationToken ct)
    {
        TEntity entity = ToEntity(aggregate);

        Set.Add(entity);

    }

    public Task<IReadOnlyList<TAggregate>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<TAggregate?> GetByIdAsync(TId id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(TId id, CancellationToken ct)
    {
        TEntity? entity = await Set.FindAsync([id], ct);

        

    }

    public Task UpdateAsync(TAggregate aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    protected abstract TEntity ToEntity(TAggregate aggregate);
    protected abstract TAggregate ToAggregate(TEntity entity);
}
