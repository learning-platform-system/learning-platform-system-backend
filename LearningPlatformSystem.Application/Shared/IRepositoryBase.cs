namespace LearningPlatformSystem.Application.Shared;

public interface IRepositoryBase<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct);

    Task<T?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct);

    Task UpdateAsync(T entity, CancellationToken ct);

    Task RemoveAsync(T entity, CancellationToken ct);
}
