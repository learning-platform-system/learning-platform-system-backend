using LearningPlatformSystem.Domain.CourseSessionAttendances;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.CourseSessionAttendances;

public class CourseSessionAttendanceTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenStudentIdIsEmpty()
    {
        Guid studentId = Guid.Empty;
        Guid courseSessionId = Guid.NewGuid();
        AttendanceStatus status = AttendanceStatus.Present;

        Action act = () => CourseSessionAttendance.Create(studentId, courseSessionId, status);

        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseSessionAttendanceErrors.StudentIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenCourseSessionIdIsEmpty()
    {
        Guid studentId = Guid.NewGuid();
        Guid courseSessionId = Guid.Empty;
        AttendanceStatus status = AttendanceStatus.Present;

        Action act = () => CourseSessionAttendance.Create(studentId, courseSessionId, status);

        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseSessionAttendanceErrors.CourseSessionIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldCreateAttendance_WhenInputIsValid()
    {
        Guid studentId = Guid.NewGuid();
        Guid courseSessionId = Guid.NewGuid();
        AttendanceStatus status = AttendanceStatus.Present;

        CourseSessionAttendance attendance = CourseSessionAttendance.Create(studentId, courseSessionId, status);

        Assert.Equal(studentId, attendance.StudentId);
        Assert.Equal(courseSessionId, attendance.CourseSessionId);
        Assert.Equal(status, attendance.Status);
    }

    [Fact]
    public void ChangeAttendanceStatus_ShouldNotChangeStatus_WhenNewStatusIsSame()
    {
        Guid studentId = Guid.NewGuid();
        Guid courseSessionId = Guid.NewGuid();
        CourseSessionAttendance attendance = CourseSessionAttendance.Rehydrate(studentId, courseSessionId, AttendanceStatus.Absent);

        attendance.ChangeAttendanceStatus(AttendanceStatus.Absent);

        Assert.Equal(AttendanceStatus.Absent, attendance.Status);
    }

    [Fact]
    public void ChangeAttendanceStatus_ShouldChangeStatus_WhenNewStatusIsDifferent()
    {
        Guid studentId = Guid.NewGuid();
        Guid courseSessionId = Guid.NewGuid();
        CourseSessionAttendance attendance = CourseSessionAttendance.Rehydrate(studentId, courseSessionId, AttendanceStatus.Absent);

        attendance.ChangeAttendanceStatus(AttendanceStatus.Present);

        Assert.Equal(AttendanceStatus.Present, attendance.Status);
    }
}
