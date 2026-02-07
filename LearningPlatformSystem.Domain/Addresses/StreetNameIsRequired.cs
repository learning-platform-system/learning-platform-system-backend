namespace LearningPlatformSystem.Domain.Addresses;

public class StreetNameIsRequired : Exception
{
    public StreetNameIsRequired() : base("Gatunamn måste anges")
    {

    }
}
