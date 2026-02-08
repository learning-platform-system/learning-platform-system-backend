namespace LearningPlatformSystem.Domain.ContactInformations;

public class PhoneNumberIsRequired : Exception
{
    public PhoneNumberIsRequired() : base("Telefonnummer måste anges.")
    {
        
    }
}