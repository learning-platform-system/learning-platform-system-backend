using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CourseRepository(LearningPlatformDbContext context) : ICourseRepository
{
    private readonly LearningPlatformDbContext _context = context;

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await _context.Courses.AnyAsync(course => course.Id == id, ct);
    }
}
