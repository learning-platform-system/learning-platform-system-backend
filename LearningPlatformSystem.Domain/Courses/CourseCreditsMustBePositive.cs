namespace LearningPlatformSystem.Domain.Courses;

public class CourseCreditsMustBePositive : Exception
{
    public CourseCreditsMustBePositive() : base("Kursens poäng måste vara större än 0.")
    {
        
    }
}