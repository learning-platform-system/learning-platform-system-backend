using LearningPlatformSystem.Application.Courses.Inputs;
using LearningPlatformSystem.Application.Courses.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Courses;

public interface ICourseService
{
    Task<ApplicationResult<Guid>> CreateCourseAsync(CreateCourseInput input, CancellationToken ct);
    Task<ApplicationResult> DeleteCourseAsync(Guid id, CancellationToken ct);
    Task<ApplicationResult<CourseOutput>> GetCourseById(Guid courseId, CancellationToken ct);
    Task<ApplicationResult<IReadOnlyList<CourseOutput>>> SearchCoursesAsync(SearchCoursesInput input, CancellationToken ct);
}
