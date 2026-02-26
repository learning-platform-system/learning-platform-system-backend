using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.CoursePeriods.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.CoursePeriods;

namespace LearningPlatformSystem.Application.CoursePeriods;

public interface ICoursePeriodService 
{
    Task<ApplicationResult> AddSessionAsync(AddCourseSessionInput input, CancellationToken ct);
    Task<ApplicationResult> AddResourceAsync(AddCoursePeriodResourceInput input, CancellationToken ct);
    Task<ApplicationResult<Guid>> CreateCoursePeriodAsync(CreateCoursePeriodInput input, CancellationToken ct);
    Task<ApplicationResult> AddReviewAsync(AddCoursePeriodReviewInput input, CancellationToken ct);
    Task<ApplicationResult> AddEnrollmentAsync(AddCoursePeriodEnrollmentInput input, CancellationToken ct);
    Task<ApplicationResult> SetCoursePeriodGradeAsync(SetGradeInput input, CancellationToken ct);
    Task<ApplicationResult> AddAttendanceAsync(AddCourseSessionAttendanceInput input, CancellationToken ct);
    Task<ApplicationResult<IReadOnlyList<CoursePeriodOutput>>> GetCoursePeriodByCourseIdAsync(Guid courseId, CancellationToken ct);
    Task<ApplicationResult> DeleteCoursePeriodAsync(Guid id, CancellationToken ct);
}
