using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CourseRepository(LearningPlatformDbContext context) : ICourseRepository
{
    private readonly LearningPlatformDbContext _context = context;

    public async Task AddAsync(Course course, CancellationToken ct)
    {
        await _context.Courses.AddAsync(new CourseEntity
        {
            Id = course.Id,
            SubcategoryId = course.SubcategoryId,
            Title = course.Title,
            Description = course.Description,
            Credits = course.Credits
        }, ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await _context.Courses.AnyAsync(course => course.Id == id, ct);
    }

    public async Task<bool> ExistsByTitleAsync(string title, CancellationToken ct)
    {
        return await _context.Courses.AnyAsync(courseEntity => courseEntity.Title == title, ct);

    }

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        CourseEntity? entity = await _context.Courses
            .AsNoTracking()
            .SingleOrDefaultAsync(course => course.Id == id, ct);

        if (entity is null) return null;

        Course course = Course.Rehydrate 
        (
            id: entity.Id,
            subcategoryId: entity.SubcategoryId,
            title: entity.Title,
            description: entity.Description,
            credits: entity.Credits
        );

        return course;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        CourseEntity? entity = await _context.Courses.SingleOrDefaultAsync(course => course.Id == id, ct);
        if (entity is null) return false;

        _context.Courses.Remove(entity);
        return true;
    }

    public Task UpdateAsync(Course aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
