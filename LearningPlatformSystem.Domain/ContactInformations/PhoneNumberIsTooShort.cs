namespace LearningPlatformSystem.Domain.ContactInformations;

public class PhoneNumberIsTooShort : Exception
{
    public PhoneNumberIsTooShort(int phoneNumberMinLength) : base($"Telefonnummer får inte vara kortare än {phoneNumberMinLength} tecken.")
    {
        
    }
}