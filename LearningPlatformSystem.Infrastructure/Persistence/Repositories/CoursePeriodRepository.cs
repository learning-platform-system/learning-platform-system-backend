using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Domain.CoursePeriodReviews;
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

    public async Task AddSessionAsync(CoursePeriod coursePeriod, CancellationToken ct)
    {
        CoursePeriodEntity entity = await _context.CoursePeriods
            .Include(cp => cp.Sessions)
            .SingleAsync(cp => cp.Id == coursePeriod.Id, ct);

        // Hitta den session som finns i aggregate-listan men ínte i entity-listan
        CourseSession newSession = coursePeriod.Sessions
            .Single(session => !entity.Sessions.Any(entitySession => entitySession.Id == session.Id));

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

    public async Task AddResourceAsync(CoursePeriod coursePeriod, CancellationToken ct)
    {
        CoursePeriodEntity entity = await _context.CoursePeriods
            .Include(cp => cp.Resources)
            .SingleAsync(cp => cp.Id == coursePeriod.Id, ct);

        CoursePeriodResource newResource = coursePeriod.Resources
            .Single(resource => !entity.Resources.Any(resourceEntity => resourceEntity.Id == resource.Id));

        entity.Resources.Add(new CoursePeriodResourceEntity
        {
            Id = newResource.Id,
            CoursePeriodId = newResource.CoursePeriodId,
            Title = newResource.Title,
            Url = newResource.Url,
            Description = newResource.Description
        });
    }

    public async Task AddReviewAsync(CoursePeriod coursePeriod, CancellationToken ct)
    {
        CoursePeriodEntity entity = await _context.CoursePeriods
            .Include(cp => cp.Reviews)
            .SingleAsync(cp => cp.Id == coursePeriod.Id, ct);

        CoursePeriodReview newReview = coursePeriod.Reviews
            .Single(review => !entity.Reviews.Any(reviewEntity => reviewEntity.Id == review.Id));
        entity.Reviews.Add(new CoursePeriodReviewEntity
        {
            Id = newReview.Id,
            CoursePeriodId = newReview.CoursePeriodId,
            StudentId = newReview.StudentId,
            Rating = newReview.Rating,
            Comment = newReview.Comment
        });
    }
}
