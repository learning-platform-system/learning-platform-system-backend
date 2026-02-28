using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.CourseSessionAttendances;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.CoursePeriods;

public class CoursePeriodTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenCourseIdIsEmpty()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid courseId = Guid.Empty;
        Guid teacherId = Guid.NewGuid();
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = start.AddDays(10);

        // Act
        Action act = () => CoursePeriod.Create(id, courseId, teacherId, start, end, CourseFormat.Onsite);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodErrors.CourseIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenTeacherIdIsEmpty()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid courseId = Guid.NewGuid();
        Guid teacherId = Guid.Empty;
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = start.AddDays(10);

        // Act
        Action act = () => CoursePeriod.Create(id, courseId, teacherId, start, end, CourseFormat.Onsite);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodErrors.TeacherIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenEndDateIsBeforeStartDate()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid courseId = Guid.NewGuid();
        Guid teacherId = Guid.NewGuid();
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = start.AddDays(-1);

        // Act
        Action act = () => CoursePeriod.Create(id, courseId, teacherId, start, end, CourseFormat.Onsite);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodErrors.InvalidPeriodDates, exception.Message);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid courseId = Guid.NewGuid();
        Guid teacherId = Guid.NewGuid();
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = start.AddDays(10);

        // Act
        CoursePeriod period = CoursePeriod.Rehydrate(id, courseId, teacherId, start, end, CourseFormat.Online);

        // Assert
        Assert.Equal(id, period.Id);
        Assert.Equal(courseId, period.CourseId);
        Assert.Equal(teacherId, period.TeacherId);
        Assert.Equal(start, period.StartDate);
        Assert.Equal(end, period.EndDate);
    }

    [Fact]
    public void ConnectToCampus_ShouldThrowDomainException_WhenCampusIdIsEmpty()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid campusId = Guid.Empty;

        // Act
        Action act = () => period.ConnectToCampus(campusId);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodErrors.CampusIdIsRequired, exception.Message);
    }

    [Fact]
    public void ConnectToCampus_ShouldThrowDomainException_WhenFormatIsOnline()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Online);
        Guid campusId = Guid.NewGuid();

        // Act
        Action act = () => period.ConnectToCampus(campusId);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodErrors.CannotConnectCampusWhenFormatOnline, exception.Message);
    }

    [Fact]
    public void AddSessionAttendance_ShouldThrowDomainException_WhenSessionNotFound()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid fakeSessionId = Guid.NewGuid();
        Guid studentId = Guid.NewGuid();

        // Act
        Action act = () => period.AddSessionAttendance(fakeSessionId, studentId, AttendanceStatus.Present);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CoursePeriodErrors.CourseSessionNotFound(fakeSessionId),
            exception.Message);
    }

    private static CoursePeriod CreateValidPeriod(CourseFormat format)
    {
        Guid id = Guid.NewGuid();
        Guid courseId = Guid.NewGuid();
        Guid teacherId = Guid.NewGuid();
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = start.AddDays(10);

        return CoursePeriod.Create(id, courseId, teacherId, start, end, format);
    }

    [Fact]
    public void EnrollStudent_ShouldThrowDomainException_WhenStudentIdIsEmpty()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.Empty;

        // Act
        Action act = () => period.EnrollStudent(studentId);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodEnrollmentErrors.StudentIdIsRequired, exception.Message);
    }

    [Fact]
    public void EnrollStudent_ShouldThrowDomainException_WhenStudentAlreadyEnrolled()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.NewGuid();
        period.EnrollStudent(studentId);

        // Act
        Action act = () => period.EnrollStudent(studentId);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodEnrollmentErrors.StudentAlreadyEnrolled, exception.Message);
    }

    [Fact]
    public void EnrollStudent_ShouldAddEnrollment_WhenValid()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.NewGuid();

        // Act
        period.EnrollStudent(studentId);

        // Assert
        Assert.Single(period.Enrollments);
    }

    [Fact]
    public void SetStudentGrade_ShouldThrowDomainException_WhenStudentIdIsEmpty()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.Empty;

        // Act
        Action act = () => period.SetStudentGrade(studentId, Grade.A);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodEnrollmentErrors.StudentIdIsRequired, exception.Message);
    }

    [Fact]
    public void SetStudentGrade_ShouldThrowDomainException_WhenStudentNotEnrolled()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.NewGuid();

        // Act
        Action act = () => period.SetStudentGrade(studentId, Grade.B);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodEnrollmentErrors.StudentNotEnrolled, exception.Message);
    }

    [Fact]
    public void AddSession_ShouldAddSession_WhenValid()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);

        // Act
        period.AddSession(
            CourseFormat.Onsite,
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.Today),
            new TimeOnly(9, 0),
            new TimeOnly(10, 0));

        // Assert
        Assert.Single(period.Sessions);
    }

    [Fact]
    public void AddReview_ShouldAddReview_WhenValid()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.NewGuid();

        // Act
        period.AddReview(studentId, Rating.Create(5), "Bra kurs");

        // Assert
        Assert.Single(period.Reviews);
    }

    [Fact]
    public void AddResource_ShouldAddResource_WhenValid()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);

        // Act
        period.AddResource("Material", "https://test.se", "Beskrivning");

        // Assert
        Assert.Single(period.Resources);
    }

    [Fact]
    public void RehydrateEnrollments_ShouldAddEnrollments()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.NewGuid();
        CoursePeriodEnrollment enrollment = CoursePeriodEnrollment.Create(studentId, period.Id);

        // Act
        period.RehydrateEnrollments(new[] { enrollment });

        // Assert
        Assert.Single(period.Enrollments);
    }

    [Fact]
    public void RehydrateReviews_ShouldAddReviews()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        Guid studentId = Guid.NewGuid();
        CoursePeriodReview review = CoursePeriodReview.Create(studentId, period.Id, Rating.Create(4), null);

        // Act
        period.RehydrateReviews(new[] { review });

        // Assert
        Assert.Single(period.Reviews);
    }

    [Fact]
    public void RehydrateResources_ShouldAddResources()
    {
        // Arrange
        CoursePeriod period = CreateValidPeriod(CourseFormat.Onsite);
        CoursePeriodResource resource = CoursePeriodResource.Create(period.Id, "Titel", "https://test.se", null);

        // Act
        period.RehydrateResources(new[] { resource });

        // Assert
        Assert.Single(period.Resources);
    }
}
