namespace LearningPlatformSystem.Domain.ContactInformations;

public class EmailIsTooLong : Exception
{
    public EmailIsTooLong(int emailMaxLength) : base($"Email får inte vara längre än {emailMaxLength} tecken.")
    {
        
    }
}