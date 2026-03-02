namespace LearningPlatformSystem.Application.Courses.Inputs;

public sealed record class CreateCourseInput(Guid SubcategoryId, string Title, string? Description, int Credits);

