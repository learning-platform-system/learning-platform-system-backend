namespace LearningPlatformSystem.Domain.Addresses;

public class PostalCodeTooLong : Exception
{
    public PostalCodeTooLong(int postalCodeMaxLength) : base($"Postkod får inte vara mer än {postalCodeMaxLength} tecken.")
    {
        
    }
}