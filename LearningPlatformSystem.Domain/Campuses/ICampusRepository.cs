namespace LearningPlatformSystem.Domain.Campuses;

public interface ICampusRepository
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);
}
