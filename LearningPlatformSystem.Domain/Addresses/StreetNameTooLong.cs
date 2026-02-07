namespace LearningPlatformSystem.Domain.Addresses;

public class StreetNameTooLong : Exception
{
    public StreetNameTooLong(int streetNameMaxLength) : base($"Gatuadressen får inte vara längre än {streetNameMaxLength} tecken.")
    {
        
    }
}