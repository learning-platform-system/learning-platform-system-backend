namespace LearningPlatformSystem.Domain.CourseSessions;

public class ClassroomIdIsRequired : Exception
{
    public ClassroomIdIsRequired() : base("Klassrums-ID måste anges.")
    {

    }
}