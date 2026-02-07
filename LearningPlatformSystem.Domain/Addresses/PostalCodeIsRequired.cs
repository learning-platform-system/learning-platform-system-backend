namespace LearningPlatformSystem.Domain.Addresses;

public class PostalCodeIsRequired : Exception
{
    public PostalCodeIsRequired() : base("Postnummer måste anges")
    {

    }
}
