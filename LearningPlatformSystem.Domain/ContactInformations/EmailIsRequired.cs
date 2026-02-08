namespace LearningPlatformSystem.Domain.ContactInformations;

public class EmailIsRequired : Exception
{
    public EmailIsRequired() : base("Email måste anges.")
    {
        
    }
}