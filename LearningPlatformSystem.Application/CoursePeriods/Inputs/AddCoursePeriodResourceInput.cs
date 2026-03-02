namespace LearningPlatformSystem.Application.CoursePeriods.Inputs;

public sealed record AddCoursePeriodResourceInput(Guid CoursePeriodId, string Title, string Url, string? Description);

