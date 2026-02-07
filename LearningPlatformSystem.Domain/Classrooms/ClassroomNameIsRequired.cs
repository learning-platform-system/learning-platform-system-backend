namespace LearningPlatformSystem.Domain.Classrooms;

public class ClassroomNameIsRequired : Exception
{
    public ClassroomNameIsRequired() : base("Klassrummets namn måste anges.")
    {
        
    }
}