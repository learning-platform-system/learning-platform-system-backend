namespace LearningPlatformSystem.Domain.Campuses;

public static class CampusErrors
{
    public const string CampusNameIsRequired = "Campusnamn måste anges.";

    public const string ContactInformationIsRequired = "Campus har ingen befintlig kontaktinformation.";

    public const string ContactInformationAlreadyExists = "Campus har redan en address.";

    public static string CampusNameIsTooLong(int campusNameMaxLength) =>
        $"Campusnamnet får inte vara längre än {campusNameMaxLength} tecken.";
}
