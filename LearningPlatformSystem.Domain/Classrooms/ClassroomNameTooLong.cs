namespace LearningPlatformSystem.Domain.Classrooms;

public class ClassroomNameTooLong : Exception
{
    public ClassroomNameTooLong(int nameMaxLength) : base($"Klassrummets namn får inte vara längre än {nameMaxLength} tecken.")
    {
        
    }
}