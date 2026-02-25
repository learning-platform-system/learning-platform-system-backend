
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Courses;

public interface ICourseRepository 
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);
}
