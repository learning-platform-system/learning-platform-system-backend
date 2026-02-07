namespace LearningPlatformSystem.Domain.Campuses;

public class CampusNameIsTooLong : Exception
{
    public CampusNameIsTooLong(int nameMaxLength) : base($"Campus namn får inte vara längre än {nameMaxLength} tecken.")
    {
        
    }
}