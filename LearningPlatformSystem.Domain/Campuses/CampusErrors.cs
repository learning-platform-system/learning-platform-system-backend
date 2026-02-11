namespace LearningPlatformSystem.Domain.Campuses;

public static class CampusErrors
{
    public const string CampusNameIsRequired = "Campusnamn måste anges.";

    public static string CampusNameIsTooLong(int campusNameMaxLength) =>
        $"Campusnamnet får inte vara längre än {campusNameMaxLength} tecken.";
}
