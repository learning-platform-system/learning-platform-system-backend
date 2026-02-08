namespace LearningPlatformSystem.Domain.Courses;

public class CourseTitleIsRequired : Exception
{
    public CourseTitleIsRequired() : base("Kursens titel måste anges.")
    {
        
    }
}