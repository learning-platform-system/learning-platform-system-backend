namespace LearningPlatformSystem.Domain.Campuses;

public class CampusNameIsRequired : Exception
{
    public CampusNameIsRequired() : base("Campus namn måste anges.")
    {
        
    }
}