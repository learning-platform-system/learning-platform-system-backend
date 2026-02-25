
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Teachers;

public interface ITeacherRepository 
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);

}
