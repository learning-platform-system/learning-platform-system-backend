namespace LearningPlatformSystem.Domain.CourseSessions;

public class CoursePeriodIdIsRequired : Exception
{
    public CoursePeriodIdIsRequired() : base("Kursperiodens ID måste anges.")
    {

    }
}