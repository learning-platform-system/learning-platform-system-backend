using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CourseSessionAttendances;
// En viss elevs närvarostatus på ett visst kurstillfälle, inom en viss kursperiod.
// PK: StudentId FK + CourseSessionId FK + CoursePeriodId FK
public sealed class CourseSessionAttendance
{
    public Guid StudentId { get; private set; }
    public Guid CourseSessionId { get; private set; }
    public AttendanceStatus Status { get; private set; }

    private CourseSessionAttendance(Guid studentId, Guid courseSessionId, AttendanceStatus status)
    {
        StudentId = studentId;
        CourseSessionId = courseSessionId;
        Status = status;
    }

    // skapande via CourseSession
    internal static CourseSessionAttendance Create(Guid studentId, Guid courseSessionId, AttendanceStatus status)
    {
        DomainValidator.ValidateRequiredGuid(studentId, CourseSessionAttendanceErrors.StudentIdIsRequired);
        DomainValidator.ValidateRequiredGuid(courseSessionId, CourseSessionAttendanceErrors.CourseSessionIdIsRequired);

        CourseSessionAttendance attendance = new(studentId, courseSessionId, status);
        return attendance;
    }

    internal static CourseSessionAttendance Rehydrate(Guid studentId, Guid courseSessionId, AttendanceStatus status)
    {
        return new CourseSessionAttendance(studentId, courseSessionId, status);
    }

    public void ChangeAttendanceStatus(AttendanceStatus newStatus)
    {
        if (Status == newStatus)
        {
            return;
        }

        Status = newStatus;
    }   
}
