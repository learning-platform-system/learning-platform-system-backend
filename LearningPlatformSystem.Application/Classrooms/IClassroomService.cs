using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Classrooms.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.Application.Classrooms;

public interface IClassroomService
{
    Task<ApplicationResult<ClassroomOutput>> CreateAsync(CreateClassroomInput input, CancellationToken ct);

    Task<ApplicationResult> DeleteAsync(Guid id, CancellationToken ct);
    Task<ApplicationResult<IReadOnlyList<ClassroomOutput>>> GetByTypeAsync(ClassroomType type, CancellationToken ct);

    //Task<Result> UpdateClassroomAsync(Guid id, UpdateClassroomInput input, CancellationToken ct);
}