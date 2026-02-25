namespace LearningPlatformSystem.API.Courses.Create;

public sealed record CreateCourseRequest(Guid SubcategoryId, string Title, string? Description, int Credits);
