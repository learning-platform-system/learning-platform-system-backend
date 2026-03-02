namespace LearningPlatformSystem.Domain.CoursePeriodEnrollments;

public class CoursePeriodEnrollmentErrors
{
    public const string StudentIdIsRequired =
        "Studentens id måste anges.";

    public const string CoursePeriodIdIsRequired =
        "Kursperiodens id måste anges.";

    public const string StudentAlreadyEnrolled =
        "Studenten är redan inskriven i denna kursperiod.";

    public const string GradeAlreadySet = "Betyg har redan satts för denna inskrivning.";

    public const string StudentNotEnrolled = "Studenten är inte inskriven i denna kursperiod.";
}
