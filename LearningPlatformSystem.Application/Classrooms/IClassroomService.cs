using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Classrooms.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.Application.Classrooms;

public interface IClassroomService
{
    Task<ApplicationResult<Guid>> CreateClassroomAsync(CreateClassroomInput input, CancellationToken ct);

    Task<ApplicationResult> DeleteClassroomAsync(Guid id, CancellationToken ct);
    Task<ApplicationResult<IReadOnlyList<ClassroomOutput>>> GetClassroomByTypeAsync(ClassroomType type, CancellationToken ct);
    Task<ApplicationResult> UpdateClassroomAsync(UpdateClassroomInput input, CancellationToken ct);
}