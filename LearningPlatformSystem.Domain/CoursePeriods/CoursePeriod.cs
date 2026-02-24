using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CoursePeriods;

public sealed class CoursePeriod
{
    // === Fields ===
    private readonly List<CourseSession> _sessions = new();
    private readonly List<CoursePeriodEnrollment> _enrollments = new();
    private readonly List<CoursePeriodReview> _reviews = new();
    private readonly List<CoursePeriodResource> _resources = new();



    // === Properties ===
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid TeacherId { get; private set; }
    public Guid? CampusId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public CourseFormat Format { get; private set; }
    public IReadOnlyCollection<CourseSession> Sessions => _sessions;
    public IReadOnlyCollection<CoursePeriodEnrollment> Enrollments => _enrollments;
    public IReadOnlyCollection<CoursePeriodReview> Reviews => _reviews;
    public IReadOnlyCollection<CoursePeriodResource> Resources => _resources;



    // === Constructor ===
    private CoursePeriod(Guid id, Guid courseId, Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        Id = id;
        CourseId = courseId;
        TeacherId = teacherId;
        StartDate = startDate;
        EndDate = endDate;
        Format = format;
    }


    // === Factory Method ===
    internal static CoursePeriod Create(Guid courseId, Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        DomainValidator.ValidateRequiredGuid(courseId, CoursePeriodErrors.CourseIdIsRequired);
        DomainValidator.ValidateRequiredGuid(teacherId, CoursePeriodErrors.TeacherIdIsRequired);

        if (endDate < startDate)
        {
            throw new DomainException(CoursePeriodErrors.InvalidPeriodDates);
        }

        Guid id = Guid.NewGuid();

        return new CoursePeriod(id, courseId, teacherId, startDate, endDate, format);
    }

    
    // === Campus ===
    public void ConnectToCampus(Guid campusId) 
    {
        DomainValidator.ValidateRequiredGuid(campusId, CoursePeriodErrors.CampusIdIsRequired);

        CampusId = campusId;
    }


    // === Sessions ===
    public void AddSession(CourseFormat format, Guid classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        CourseSession session = CourseSession.Create(this.Id, format, classroomId, date, startTime, endTime);

        _sessions.Add(session);
    }


    // === Enrollments ===
    public void EnrollStudent(Guid studentId)
    {
        DomainValidator.ValidateRequiredGuid(studentId, CoursePeriodEnrollmentErrors.StudentIdIsRequired);

        // Dubbelregistreringskontroll
        if (_enrollments.Any(enrollment => enrollment.StudentId == studentId))
        {
            throw new DomainException(CoursePeriodEnrollmentErrors.StudentAlreadyEnrolled);
        }

        var enrollment = CoursePeriodEnrollment.Create(this.Id, studentId);

        _enrollments.Add(enrollment);
    }


    // === Reviews ===
    public void AddReview(Guid studentId, int ratingValue, string? comment)
    {
        Rating rating = Rating.Create(ratingValue);

        CoursePeriodReview review = CoursePeriodReview.Create(this.Id, studentId, rating, comment);
        _reviews.Add(review);
    }


    // === Resources ===
    public void AddResource(string title, string url, string? description)
    {
        CoursePeriodResource resource = CoursePeriodResource.Create(this.Id, title, url, description);
        _resources.Add(resource);
    }

    public void RemoveResource(Guid resourceId)
    {
        CoursePeriodResource? resource = _resources.FirstOrDefault(r => r.Id == resourceId);
        if (resource is null)
        {
            throw new DomainException(CoursePeriodResourceErrors.CoursePeriodResourceNotFound);
        }

        _resources.Remove(resource);
    }
}
