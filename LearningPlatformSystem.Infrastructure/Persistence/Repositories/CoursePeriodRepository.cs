using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task UpdateAsync(CoursePeriod aggregate, CancellationToken ct)
    {
        CoursePeriodEntity cPeriodEntity  = await _context.CoursePeriods.FirstAsync(cp => cp.Id == aggregate.Id, ct);

        cPeriodEntity.StartDate = aggregate.StartDate;
        cPeriodEntity.EndDate = aggregate.EndDate;
    }

    public async Task AddSessionAsync(CoursePeriod aggregate, CancellationToken ct)
    {
        CoursePeriodEntity entity = await _context.CoursePeriods
            .Include(cp => cp.Sessions)
            .SingleAsync(cp => cp.Id == aggregate.Id, ct);

        // Hitta den session som finns i aggregate-listan men ínte i entity-listan
        CourseSession newSession = aggregate.Sessions
            .Single(s => !entity.Sessions.Any(e => e.Id == s.Id));

        entity.Sessions.Add(new CourseSessionEntity
        {
            Id = newSession.Id,
            CoursePeriodId = newSession.CoursePeriodId,
            ClassroomId = newSession.ClassroomId,
            Date = newSession.Date,
            StartTime = newSession.StartTime,
            EndTime = newSession.EndTime,
            Format = newSession.Format
        });
    }
}
