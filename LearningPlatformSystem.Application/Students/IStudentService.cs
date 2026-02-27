using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students.Inputs;

namespace LearningPlatformSystem.Application.Students;

public interface IStudentService
{
    Task<ApplicationResult<Guid>> CreateStudentAsync(CreateStudentInput input, CancellationToken ct);
}