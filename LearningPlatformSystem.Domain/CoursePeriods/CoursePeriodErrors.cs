namespace LearningPlatformSystem.Domain.CoursePeriods;

public static class CoursePeriodErrors
{
    public const string CampusIdIsRequired = "Campus ID måste anges.";
    public const string TeacherIdIsRequired = "Lärarens ID måste anges.";
    public const string CourseIdIsRequired = "Kursens ID måste anges.";
    public const string InvalidPeriodDates = "Periodens slutdatum måste vara efter startdatum.";

    public static string CourseSessionNotFound(Guid sessionId) => $"Kursperiodssession med ID {sessionId} hittades inte.";
}
