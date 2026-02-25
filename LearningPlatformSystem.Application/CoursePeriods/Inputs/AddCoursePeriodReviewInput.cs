using LearningPlatformSystem.Domain.CoursePeriodReviews;

namespace LearningPlatformSystem.Application.CoursePeriods.Inputs;

public sealed record AddCoursePeriodReviewInput(Guid StudentId, Guid CoursePeriodId, Rating Rating, string? Comment);
