using LearningPlatformSystem.Domain.Teachers;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class TeacherRepository(LearningPlatformDbContext context) : ITeacherRepository
{
    private readonly LearningPlatformDbContext _context = context;

    public Task AddAsync(Teacher aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Guid teacherId, CancellationToken ct)
    {
        return await _context.Teachers.AnyAsync(teacher => teacher.Id == teacherId, ct);
    }

    public Task<Teacher?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Teacher aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
