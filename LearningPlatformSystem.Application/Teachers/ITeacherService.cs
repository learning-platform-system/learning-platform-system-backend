using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers.Inputs;
using LearningPlatformSystem.Application.Teachers.Outputs;

namespace LearningPlatformSystem.Application.Teachers;

public interface ITeacherService
{
    Task<ApplicationResult> AddTeacherAddressAsync(AddTeacherAddressInput input, CancellationToken ct);
    Task<ApplicationResult<Guid>> CreateTeacherAsync(CreateTeacherInput input, CancellationToken ct);
    Task<ApplicationResult> DeleteTeacherAsync(Guid id, CancellationToken ct);
    Task<ApplicationResult<IReadOnlyList<TeacherOutput>>> GetAllTeachersAsync(CancellationToken ct);
}