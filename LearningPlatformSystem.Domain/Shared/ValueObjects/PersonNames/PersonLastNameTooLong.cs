namespace LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

public class PersonLastNameTooLong : Exception
{
    public PersonLastNameTooLong(int lastNameMaxLength) : base($"Efternamnet får inte vara längre än {lastNameMaxLength} tecken.")
    {

    }
}