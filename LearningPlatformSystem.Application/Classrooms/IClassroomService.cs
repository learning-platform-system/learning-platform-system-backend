using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Classrooms;

public interface IClassroomService
{
    Task<ApplicationResult> CreateAsync(CreateClassroomInput input, CancellationToken ct);

    //Task<Result> UpdateClassroomAsync(Guid id, UpdateClassroomInput input, CancellationToken ct);
}