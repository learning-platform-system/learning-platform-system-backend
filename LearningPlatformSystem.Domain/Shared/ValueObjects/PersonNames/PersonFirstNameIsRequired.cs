namespace LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

public class PersonFirstNameIsRequired : Exception
{
    public PersonFirstNameIsRequired() : base("Förnamn måste anges.")
    {
        
    }
}