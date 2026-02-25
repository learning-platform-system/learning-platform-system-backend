using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.CoursePeriods;

public interface ICoursePeriodService 
{
    Task<ApplicationResult> AddSessionAsync(AddCourseSessionInput input, CancellationToken ct);
    Task<ApplicationResult> AddResourceAsync(AddCoursePeriodResourceInput input, CancellationToken ct);
    Task<ApplicationResult<Guid>> CreateAsync(CreateCoursePeriodInput input, CancellationToken ct);
    Task<ApplicationResult> AddReviewAsync(AddCoursePeriodReviewInput input, CancellationToken ct);
    Task<ApplicationResult> AddEnrollmentAsync(AddCoursePeriodEnrollmentInput input, CancellationToken ct);
    //Task<ApplicationResult> DeleteAsync(Guid id, CancellationToken ct);
    //Task<ApplicationResult<CoursePeriod?>> GetByIdAsync(Guid id, CancellationToken ct);
    //Task<ApplicationResult> UpdateAsync(CoursePeriod input, CancellationToken ct);
}
