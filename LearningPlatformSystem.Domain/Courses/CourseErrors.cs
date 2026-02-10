namespace LearningPlatformSystem.Domain.Courses;

public static class CourseErrors
{
    public static string CreditsValueOutOfRange(int creditsMinValue, int creditsMaxValue) =>
        $"Antalet poäng måste vara mellan {creditsMinValue} och {creditsMaxValue}.";

    public const string CourseTitleIsRequired = "Kurstitel måste anges.";
    public static string CourseTitleIsTooLong(int courseTitleMaxLength) => $"Kurstiteln får inte vara ängre än {courseTitleMaxLength} tecken.";

    public const string SubcategoryIdIsRequired = "Underkategori måste anges.";
}
