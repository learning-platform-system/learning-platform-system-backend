using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.CoursePeriods;

public interface ICoursePeriodRepository : IRepositoryBase<CoursePeriod, Guid>
{
    Task AddSessionAsync(CoursePeriod aggregate, CancellationToken ct);
}
