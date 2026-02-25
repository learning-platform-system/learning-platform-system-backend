using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Courses;

public interface ICourseRepository : IRepositoryBase<Course, Guid>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsByTitleAsync(string title, CancellationToken ct);
}
