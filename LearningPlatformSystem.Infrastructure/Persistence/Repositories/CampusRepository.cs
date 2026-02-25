using LearningPlatformSystem.Domain.Campuses;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CampusRepository : ICampusRepository
{
    public Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
