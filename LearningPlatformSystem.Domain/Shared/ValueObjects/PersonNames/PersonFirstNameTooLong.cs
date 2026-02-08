namespace LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

public class PersonFirstNameTooLong : Exception 
{
    public PersonFirstNameTooLong(int firstNameMaxLength) : base($"Förnamnet får inte vara längre än {firstNameMaxLength} tecken.")
    {
        
    }
}