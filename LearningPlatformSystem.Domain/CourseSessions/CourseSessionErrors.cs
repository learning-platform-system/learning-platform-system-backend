namespace LearningPlatformSystem.Domain.CourseSessions;

public class CourseSessionErrors
{
    public const string CoursePeriodIdIsRequired = "Kursperiod måste anges.";
    public const string ClassroomIdIsRequired = "Lokal måste anges.";
    public const string CourseSessionEndTimeMustBeAfterStartTime = "Sluttiden måste vara efter starttiden.";

    public const string ClassroomIdIsRequiredForOnsiteSession = "För onsite-format måste ett klassrum anges.";

    public const string ClassroomNotAllowedForOnlineSession = "För online-format får inget klassrum anges.";
}
