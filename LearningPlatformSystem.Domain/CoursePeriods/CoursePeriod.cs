using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Domain.CourseSessionAttendances;
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
    public static CoursePeriod Create(Guid id, Guid courseId, Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        DomainValidator.ValidateRequiredGuid(courseId, CoursePeriodErrors.CourseIdIsRequired);
        DomainValidator.ValidateRequiredGuid(teacherId, CoursePeriodErrors.TeacherIdIsRequired);

        if (endDate < startDate)
        {
            throw new DomainException(CoursePeriodErrors.InvalidPeriodDates);
        }

        return new CoursePeriod(id, courseId, teacherId, startDate, endDate, format);
    }

    internal static CoursePeriod Rehydrate(Guid id, Guid courseId, Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        return new CoursePeriod(id, courseId, teacherId, startDate, endDate, format);
    }


    // === Campus ===
    public void ConnectToCampus(Guid campusId) 
    {
        DomainValidator.ValidateRequiredGuid(campusId, CoursePeriodErrors.CampusIdIsRequired);

        if (Format == CourseFormat.Online)
        {
            throw new DomainException(CoursePeriodErrors.CannotConnectCampusWhenFormatOnline);
        }

        CampusId = campusId;
    }


    // === Sessions ===
    public void AddSession(CourseFormat format, Guid? classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        CourseSession session = CourseSession.Create(this.Id, format, classroomId, date, startTime, endTime);

        _sessions.Add(session);
    }

    internal void RehydrateSessions(IEnumerable<CourseSession> sessions)
    {
        _sessions.AddRange(sessions);
    }

    public void AddSessionAttendance(Guid courseSessionId, Guid studentId, AttendanceStatus status)
    {
        CourseSession? session = _sessions.SingleOrDefault(session => session.Id == courseSessionId);

        if (session is null)
        {
            throw new DomainException(CoursePeriodErrors.CourseSessionNotFound(courseSessionId));
        }

        session.AddAttendance(studentId, status);
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

    // === Rehydrering enrollments ===
    internal void RehydrateEnrollments(IEnumerable<CoursePeriodEnrollment> enrollments)
    {
        _enrollments.AddRange(enrollments);
    }

    public void SetStudentGrade(Guid studentId, Grade grade)
    {
        DomainValidator.ValidateRequiredGuid(studentId, CoursePeriodEnrollmentErrors.StudentIdIsRequired);

        CoursePeriodEnrollment? enrollment = _enrollments.SingleOrDefault(enrollment => enrollment.StudentId == studentId);

        if (enrollment is null)
        {
            throw new DomainException(CoursePeriodEnrollmentErrors.StudentNotEnrolled);
        }

        enrollment.SetGrade(grade);
    }

    // === Reviews ===
    public void AddReview(Guid studentId, int ratingValue, string? comment)
    {
        Rating rating = Rating.Create(ratingValue);

        CoursePeriodReview review = CoursePeriodReview.Create(this.Id, studentId, rating, comment);
        _reviews.Add(review);
    }

    internal void RehydrateReviews(IEnumerable<CoursePeriodReview> reviews)
    {
        _reviews.AddRange(reviews);
    }   


    // === Resources ===
    public void AddResource(string title, string url, string? description)
    {
        CoursePeriodResource resource = CoursePeriodResource.Create(this.Id, title, url, description);
        _resources.Add(resource);
    }

    internal void RehydrateResources(IEnumerable<CoursePeriodResource> resources)
    {
        _resources.AddRange(resources);
    }
}
