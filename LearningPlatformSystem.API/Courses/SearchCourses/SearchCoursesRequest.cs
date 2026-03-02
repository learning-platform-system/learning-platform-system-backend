namespace LearningPlatformSystem.API.Courses.SearchCourses;

public sealed class SearchCoursesRequest
{
    public string? Title { get; init; }
    public Guid? SubcategoryId { get; init; }
}
