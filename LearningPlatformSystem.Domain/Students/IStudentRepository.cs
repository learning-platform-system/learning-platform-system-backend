
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Students;

public interface IStudentRepository : IRepositoryBase<Student, Guid>
{
    Task<bool> ExistsWithTheSameEmailAsync(string email, CancellationToken ct);
}
