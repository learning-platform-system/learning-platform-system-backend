using LearningPlatformSystem.Domain.CoursePeriodEnrollments;

namespace LearningPlatformSystem.Application.CoursePeriods.Inputs;

public sealed record SetGradeInput(Guid CoursePeriodId, Guid StudentId, Grade Grade);
