namespace LearningPlatformSystem.Domain.ContactInformations;

public class PhoneNumberIsTooLong : Exception
{
    public PhoneNumberIsTooLong(int phoneNumberMaxLength) : base($"Telefonnummer får inte vara längre än {phoneNumberMaxLength} tecken.")
    {
        
    }
}