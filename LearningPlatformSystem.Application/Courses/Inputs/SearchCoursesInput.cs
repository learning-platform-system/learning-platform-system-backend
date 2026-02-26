namespace LearningPlatformSystem.Application.Courses.Inputs;

public sealed class SearchCoursesInput
{
    public string? Title { get; init; }
    public Guid? SubcategoryId { get; init; }
}
