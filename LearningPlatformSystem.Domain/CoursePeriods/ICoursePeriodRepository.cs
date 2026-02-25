using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.CoursePeriods;

public interface ICoursePeriodRepository : IRepositoryBase<CoursePeriod, Guid>
{
    Task AddResourceAsync(CoursePeriod coursePeriod, CancellationToken ct);
    Task AddSessionAsync(CoursePeriod aggregate, CancellationToken ct);
}
