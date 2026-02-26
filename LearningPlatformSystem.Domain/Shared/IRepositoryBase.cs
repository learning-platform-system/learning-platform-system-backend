namespace LearningPlatformSystem.Domain.Shared;

public interface IRepositoryBase<TAggregate, TId> 
{
    Task AddAsync(TAggregate aggregate, CancellationToken ct);

    Task<TAggregate?> GetByIdAsync(TId id, CancellationToken ct);
}
