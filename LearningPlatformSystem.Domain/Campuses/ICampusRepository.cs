using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Campuses;

public interface ICampusRepository : IRepositoryBase<Campus, Guid>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);
    Task<IReadOnlyList<Campus>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(Campus campus, CancellationToken ct);
}
