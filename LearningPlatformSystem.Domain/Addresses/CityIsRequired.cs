namespace LearningPlatformSystem.Domain.Addresses;

public class CityIsRequired : Exception
{
    public CityIsRequired() : base("Postort måste anges.")
    {
        
    }
}