namespace LearningPlatformSystem.API.CoursePeriods.SetGrade;

public sealed record SetGradeRequest(Guid StudentId, string Grade);
