using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.CourseSessionAttendances;
using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using LearningPlatformSystem.Infrastructure.Persistence.Mapping.CoursePeriodMappings;
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

    public async Task AddEnrollmentAsync(CoursePeriod coursePeriod, CancellationToken ct)
    {
        CoursePeriodEntity entity = await _context.CoursePeriods
            .Include(cp => cp.Enrollments)
            .SingleAsync(cp => cp.Id == coursePeriod.Id, ct);

        CoursePeriodEnrollment newEnrollment = coursePeriod.Enrollments
            .Single(enrollment => !entity.Enrollments.Any(enrollmentEntity => enrollmentEntity.StudentId == enrollment.StudentId));

        entity.Enrollments.Add(new CoursePeriodEnrollmentEntity 
        { 
            StudentId = newEnrollment.StudentId, 
            CoursePeriodId = newEnrollment.CoursePeriodId, 
            Grade = newEnrollment.Grade 
        });
    }

    public async Task<CoursePeriod?> GetByIdWithEnrollmentsAsync(Guid coursePeriodId, CancellationToken ct)
    {
        CoursePeriodEntity? entity = await _context.CoursePeriods
            .Include(cp => cp.Enrollments)
            .SingleOrDefaultAsync(cp => cp.Id == coursePeriodId, ct);

        if (entity is null)
            return null;

        CoursePeriod coursePeriod = entity.ToDomainModel();

        IEnumerable<CoursePeriodEnrollment> enrollments = entity.Enrollments.ToDomainModel();

        coursePeriod.RehydrateEnrollments(enrollments);        

        return coursePeriod;
    }

    public async Task<CoursePeriod?> GetByIdWithResourcesAsync(Guid coursePeriodId, CancellationToken ct)
    {
        CoursePeriodEntity? entity = await _context.CoursePeriods
            .Include(cp => cp.Resources)
            .SingleOrDefaultAsync(cp => cp.Id == coursePeriodId, ct);

        if (entity is null) return null;

        // Mappar grunddata
        CoursePeriod coursePeriod = entity.ToDomainModel();

        IEnumerable<CoursePeriodResource> resources = entity.Resources.ToDomainModel();

        // lägg till i coursePeriods recource-domainlista 
        coursePeriod.RehydrateResources(resources);

        return coursePeriod;
    }

    public async Task<CoursePeriod?> GetByIdWithReviewsAsync(Guid coursePeriodId, CancellationToken ct)
    {
        CoursePeriodEntity? entity = await _context.CoursePeriods
            .Include(cp => cp.Reviews)
            .SingleOrDefaultAsync(cp => cp.Id == coursePeriodId, ct);

        if (entity is null) return null;

        CoursePeriod coursePeriod = entity.ToDomainModel();

        IEnumerable<CoursePeriodReview> reviews = entity.Reviews.ToDomainModel();

        coursePeriod.RehydrateReviews(reviews);

        return coursePeriod;
    }

    public async Task UpdateEnrollmentAsync(CoursePeriod coursePeriod, CancellationToken ct)
    {
        CoursePeriodEntity entity = await _context.CoursePeriods
            .Include(cp => cp.Enrollments)
            .SingleAsync(cp => cp.Id == coursePeriod.Id, ct);

        foreach (CoursePeriodEnrollmentEntity enrollmentEntity in entity.Enrollments)
        {
            // Hämta motsvarande domainEnrollment via id och uppdatera grade i entityn
            enrollmentEntity.Grade = coursePeriod.Enrollments.Single(domainEnrollment => domainEnrollment.StudentId == enrollmentEntity.StudentId).Grade;
        }
    }

    public async Task<CoursePeriod?> GetByIdWithSessionsAsync(Guid coursePeriodId, CancellationToken ct)
    {
        CoursePeriodEntity? entity = await _context.CoursePeriods
            .Include(cp => cp.Sessions)
            .SingleOrDefaultAsync(cp => cp.Id == coursePeriodId, ct);

        if (entity is null) return null;

        CoursePeriod coursePeriod = entity.ToDomainModel();

        IEnumerable<CourseSession> sessions = entity.Sessions.ToDomainModel();

        coursePeriod.RehydrateSessions(sessions);

        return coursePeriod;
    }

    public async Task AddSessionAttendanceAsync(CoursePeriod coursePeriod, CancellationToken ct)
    {
        // Hämtar motsvarande entity med inkluderade sessions (tomma attendance-listor)
        CoursePeriodEntity entity = await _context.CoursePeriods
            .Include(cp => cp.Sessions)
            .SingleAsync(cPEntity => cPEntity.Id == coursePeriod.Id);

        // Hittar den session som fått en ny attendance i den aktuella coursePeriod. 
        CourseSession domainSession = coursePeriod.Sessions.First(session => session.Attendances.Any());

        // Hämtar den attendance som finns i sessionen (finns bara en)
        CourseSessionAttendance attendance = domainSession.Attendances.First();

        CourseSessionEntity sessionEntity = entity.Sessions.First(sessionEntity => sessionEntity.Id == domainSession.Id);

        sessionEntity.Attendances.Add(new CourseSessionAttendanceEntity
        {
            StudentId = attendance.StudentId,
            CourseSessionId = sessionEntity.Id,
            Status = attendance.Status
        });

    }

}
