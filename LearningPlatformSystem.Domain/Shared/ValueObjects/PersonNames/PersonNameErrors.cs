namespace LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

public static class PersonNameErrors
{
    public const string FirstNameIsRequired = "Förnamn måste anges.";
    public static string FirstNameIsTooLong(int firstNameMaxLength) => $"Förnamnet får inte vara längre än {firstNameMaxLength} tecken.";

    
    public const string LastNameIsRequired = "Efternamn måste anges.";
    public static string LastNameIsTooLong(int lastNameMaxLength) => $"Efternamnet får inte vara längre än {lastNameMaxLength} tecken.";
}
