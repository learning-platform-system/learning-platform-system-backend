using LearningPlatformSystem.Domain.CourseSessionAttendances;
using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.Aggregates;

public class CourseSessionTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenEndTimeIsBeforeOrEqualStartTime()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        CourseFormat format = CourseFormat.Online;
        Guid? classroomId = null;

        DateOnly date = new(2026, 1, 10);
        TimeOnly startTime = new(10, 0);
        TimeOnly endTime = new(10, 0);

        // Act
        Action act = () => CourseSession.Create(coursePeriodId, format, classroomId, date, startTime, endTime);

        // Assert - använder DITT error
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseSessionErrors.CourseSessionEndTimeMustBeAfterStartTime, exception.Message);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();
        CourseFormat format = CourseFormat.Onsite;
        Guid classroomId = Guid.NewGuid();

        DateOnly date = new(2026, 2, 1);
        TimeOnly startTime = new(9, 0);
        TimeOnly endTime = new(12, 0);

        // Act
        CourseSession session = CourseSession.Rehydrate(id, coursePeriodId, format, classroomId, date, startTime, endTime);

        // Assert
        Assert.Equal(id, session.Id);
        Assert.Equal(coursePeriodId, session.CoursePeriodId);
        Assert.Equal(format, session.Format);
        Assert.Equal(classroomId, session.ClassroomId);
        Assert.Equal(date, session.Date);
        Assert.Equal(startTime, session.StartTime);
        Assert.Equal(endTime, session.EndTime);
    }

    [Fact]
    public void AddAttendance_ShouldThrowDomainException_WhenAttendanceAlreadyExistsForStudent()
    {
        // Arrange
        Guid sessionId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();

        CourseSession session = CourseSession.Rehydrate(
            sessionId,
            coursePeriodId,
            CourseFormat.Online,
            null,
            new DateOnly(2026, 2, 1),
            new TimeOnly(9, 0),
            new TimeOnly(10, 0)
        );

        Guid studentId = Guid.NewGuid();

        session.AddAttendance(studentId, AttendanceStatus.Present);

        // Act
        Action act = () => session.AddAttendance(studentId, AttendanceStatus.Present);

        // Assert - använder DITT error
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseSessionAttendanceErrors.AttendanceAlreadyRegistered, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenFormatIsOnsite_AndClassroomIdIsNull()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        CourseFormat format = CourseFormat.Onsite;
        Guid? classroomId = null;

        DateOnly date = new(2026, 2, 1);
        TimeOnly startTime = new(9, 0);
        TimeOnly endTime = new(10, 0);

        // Act
        Action act = () => CourseSession.Create(coursePeriodId, format, classroomId, date, startTime, endTime);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseSessionErrors.ClassroomIdIsRequiredForOnsiteSession, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenFormatIsOnline_AndClassroomIdIsProvided()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        CourseFormat format = CourseFormat.Online;
        Guid? classroomId = Guid.NewGuid();

        DateOnly date = new(2026, 2, 1);
        TimeOnly startTime = new(9, 0);
        TimeOnly endTime = new(10, 0);

        // Act
        Action act = () => CourseSession.Create(coursePeriodId, format, classroomId, date, startTime, endTime);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseSessionErrors.ClassroomNotAllowedForOnlineSession, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenFormatIsOnsite_AndClassroomIdIsEmptyGuid()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        CourseFormat format = CourseFormat.Onsite;
        Guid? classroomId = Guid.Empty;

        DateOnly date = new(2026, 2, 1);
        TimeOnly startTime = new(9, 0);
        TimeOnly endTime = new(10, 0);

        // Act
        Action act = () => CourseSession.Create(coursePeriodId, format, classroomId, date, startTime, endTime);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseSessionErrors.ClassroomIdIsRequiredForOnsiteSession, exception.Message);
    }
}
