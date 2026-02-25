using LearningPlatformSystem.Domain.Teachers;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class TeacherRepository : ITeacherRepository
{
    public Task<bool> ExistsAsync(Guid teacherId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
