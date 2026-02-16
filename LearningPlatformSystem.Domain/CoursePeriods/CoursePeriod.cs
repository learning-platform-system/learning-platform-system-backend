using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CoursePeriods;

public class CoursePeriod
{
    private readonly List<CourseSession> _sessions = new();
    private readonly List<CoursePeriodEnrollment> _enrollments = new();
    private readonly List<CoursePeriodReview> _reviews = new();

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

    private CoursePeriod(Guid id, Guid courseId, Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        Id = id;
        CourseId = courseId;
        TeacherId = teacherId;
        StartDate = startDate;
        EndDate = endDate;
        Format = format;
    }

    // Ingen CoursePeriod kan skapas utan att ligga i Course._coursePeriods.
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

    public void ConnectToCampus(Guid campusId) 
    {
        DomainValidator.ValidateRequiredGuid(campusId, CoursePeriodErrors.CampusIdIsRequired);

        CampusId = campusId;
    }

    public void AddSession(Guid classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        CourseSession session = CourseSession.Create(this.Id, classroomId, date, startTime, endTime);

        _sessions.Add(session);
    }

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

    public void AddReview(Guid studentId, int ratingValue, string? comment)
    {
        Rating rating = Rating.Create(ratingValue);

        CoursePeriodReview review = CoursePeriodReview.Create(this.Id, studentId, rating, comment);
        _reviews.Add(review);
    }
}
