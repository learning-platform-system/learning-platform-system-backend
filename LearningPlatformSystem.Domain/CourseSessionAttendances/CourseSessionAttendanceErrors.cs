namespace LearningPlatformSystem.Domain.CourseSessionAttendances;

public class CourseSessionAttendanceErrors
{
    public const string StudentIdIsRequired = "Student-id måste anges";
    public const string CourseSessionIdIsRequired = "Kurstillfällets id måste anges";
    public const string CoursePeriodIdIsRequired = "Kursperiodens id måste anges";
    public const string AttendanceAlreadyRegistered = "Närvaro är redan registrerad för denna student på detta kurstillfälle";
}
