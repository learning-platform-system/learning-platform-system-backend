using LearningPlatformSystem.Domain.CourseSessionAttendances;

namespace LearningPlatformSystem.Domain.CourseSessions;

public class CourseSessionErrors
{
    public const string CoursePeriodIdIsRequired = "Kursperiod måste anges.";
    public const string ClassroomIdIsRequired = "Lokal måste anges.";
    public const string CourseSessionEndTimeMustBeAfterStartTime = "Sluttiden måste vara efter starttiden.";


    public void bla()
    {
        var courseSession = CourseSession.Create(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now.AddHours(1)));
        var studentId = Guid.NewGuid();

        courseSession.AddAttendance(studentId, AttendanceStatus.Present);

       var currentCourseSession = courseSession.Attendances.First(a => a.StudentId == studentId);

        currentCourseSession.ChangeAttendanceStatus(AttendanceStatus.Absent);
    }
}
