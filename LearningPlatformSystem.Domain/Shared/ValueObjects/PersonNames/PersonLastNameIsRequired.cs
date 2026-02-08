
namespace LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

public class PersonLastNameIsRequired : Exception
{
    public PersonLastNameIsRequired() : base("Efternamn måste anges.")
    {
        
    }
}