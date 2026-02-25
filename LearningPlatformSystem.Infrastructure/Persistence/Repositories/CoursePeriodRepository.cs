using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CoursePeriodRepository(LearningPlatformDbContext context) : ICoursePeriodRepository
{
    private readonly LearningPlatformDbContext _context = context;

    public async Task AddAsync(CoursePeriod aggregate, CancellationToken ct)
    {
        CoursePeriodEntity coursePeriodEntity = new CoursePeriodEntity
        {
            Id = aggregate.Id,
            CampusId = aggregate.CampusId, // blir null om campus inte finns
            CourseId = aggregate.CourseId,
            EndDate = aggregate.EndDate,
            StartDate = aggregate.StartDate,
            Format = aggregate.Format,
            TeacherId = aggregate.TeacherId
        };

        await _context.CoursePeriods.AddAsync(coursePeriodEntity, ct);
    }

    public Task<CoursePeriod?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CoursePeriod aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
