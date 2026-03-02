namespace LearningPlatformSystem.Application.Courses.Outputs;

public sealed record CourseOutput(Guid Id, Guid SubcategoryId, string Title, string? Description, int Credits);
