using LearningPlatformSystem.Application.Courses.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Courses;

public interface ICourseService
{
    Task<ApplicationResult<Guid>> CreateAsync(CreateCourseInput input, CancellationToken ct);
}
