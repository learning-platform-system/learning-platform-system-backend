
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Teachers;

public interface ITeacherRepository : IRepositoryBase<Teacher, Guid>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsWithTheSameEmailAsync(string email, CancellationToken ct);
    Task UpdateAsync(Teacher teacher, CancellationToken ct);
}
