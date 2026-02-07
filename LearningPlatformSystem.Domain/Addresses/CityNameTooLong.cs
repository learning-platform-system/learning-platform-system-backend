namespace LearningPlatformSystem.Domain.Addresses;

public class CityNameTooLong : Exception
{
    public CityNameTooLong(int cityNameMaxLength) : base($"Postort får inte vara längre än {cityNameMaxLength} tecken.")
    {
        
    }
}