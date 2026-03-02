using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.CoursePeriods;

public interface ICoursePeriodRepository : IRepositoryBase<CoursePeriod, Guid>
{
    Task AddEnrollmentAsync(CoursePeriod coursePeriod, CancellationToken ct);
    Task AddResourceAsync(CoursePeriod coursePeriod, CancellationToken ct);
    Task AddReviewAsync(CoursePeriod coursePeriod, CancellationToken ct);
    Task AddSessionAsync(CoursePeriod aggregate, CancellationToken ct);
    Task AddSessionAttendanceAsync(CoursePeriod coursePeriod, CancellationToken ct);
    Task<IReadOnlyList<CoursePeriod>> GetByCourseIdAsync(Guid courseId, CancellationToken ct);
    Task<CoursePeriod?> GetByIdWithEnrollmentsAsync(Guid coursePeriodId, CancellationToken ct);
    Task<CoursePeriod?> GetByIdWithResourcesAsync(Guid coursePeriodId, CancellationToken ct);
    Task<CoursePeriod?> GetByIdWithReviewsAsync(Guid coursePeriodId, CancellationToken ct);
    Task<CoursePeriod?> GetByIdWithSessionsAsync(Guid coursePeriodId, CancellationToken ct);
    Task UpdateEnrollmentAsync(CoursePeriod coursePeriod, CancellationToken ct);
}
