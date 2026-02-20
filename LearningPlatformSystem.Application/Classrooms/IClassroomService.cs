using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Shared.Results;

namespace LearningPlatformSystem.Application.Classrooms;

public interface IClassroomService
{
    Task<Result> CreateClassroomAsync(CreateClassroomInput input, CancellationToken ct);

    //Task<Result> UpdateClassroomAsync(Guid id, UpdateClassroomInput input, CancellationToken ct);
}