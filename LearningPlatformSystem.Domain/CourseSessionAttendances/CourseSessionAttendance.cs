using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CourseSessionAttendances;
// En viss elevs närvarostatus på ett visst kurstillfälle, inom en viss kursperiod.
// PK: StudentId FK + CourseSessionId FK + CoursePeriodId FK
public class CourseSessionAttendance
{
    private CourseSessionAttendance() { } // parameterlös konstruktor som krävs av EF Core

    public Guid StudentId { get; private set; }
    public Guid CourseSessionId { get; private set; }
    public Guid CoursePeriodId { get; private set; }
    public AttendanceStatus Status { get; private set; }

    private CourseSessionAttendance(Guid studentId, Guid courseSessionId, Guid coursePeriodId, AttendanceStatus status)
    {
        StudentId = studentId;
        CourseSessionId = courseSessionId;
        CoursePeriodId = coursePeriodId;
        Status = status;
    }

    // skapande via CourseSession
    internal static CourseSessionAttendance Create(Guid studentId, Guid courseSessionId, Guid coursePeriodId, AttendanceStatus status)
    {
        DomainValidator.ValidateRequiredGuid(studentId, CourseSessionAttendanceErrors.StudentIdIsRequired);
        DomainValidator.ValidateRequiredGuid(courseSessionId, CourseSessionAttendanceErrors.CourseSessionIdIsRequired);
        DomainValidator.ValidateRequiredGuid(coursePeriodId, CourseSessionAttendanceErrors.CoursePeriodIdIsRequired);

        CourseSessionAttendance attendance = new(studentId, courseSessionId, coursePeriodId, status);
        return attendance;
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
