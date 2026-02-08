
namespace LearningPlatformSystem.Domain.Courses;
public class CourseTitleTooLong : Exception
{
    public CourseTitleTooLong(int titleMaxLength) : base($"Kursens titel får inte vara längre än {titleMaxLength} tecken.")
    {

    }
}