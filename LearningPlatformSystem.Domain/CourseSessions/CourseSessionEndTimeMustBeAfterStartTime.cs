namespace LearningPlatformSystem.Domain.CourseSessions;

public class CourseSessionEndTimeMustBeAfterStartTime : Exception
{
    public CourseSessionEndTimeMustBeAfterStartTime() : base("Kursens sluttid måste vara efter starttid.")
    {

    }
}