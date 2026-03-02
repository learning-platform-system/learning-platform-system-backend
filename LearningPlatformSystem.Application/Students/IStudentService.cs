using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students.Inputs;
using LearningPlatformSystem.Application.Students.Outputs;

namespace LearningPlatformSystem.Application.Students;

public interface IStudentService
{
    Task<ApplicationResult> AddStudentAddressAsync(AddStudentAddressInput input, CancellationToken ct);
    Task<ApplicationResult<Guid>> CreateStudentAsync(CreateStudentInput input, CancellationToken ct);
    Task<ApplicationResult> DeleteStudentAsync(Guid id, CancellationToken ct);
    Task<ApplicationResult<IReadOnlyList<StudentOutput>>> GetAllStudentsAsync(CancellationToken ct);
}