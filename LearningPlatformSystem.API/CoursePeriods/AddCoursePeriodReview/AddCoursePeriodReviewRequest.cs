using LearningPlatformSystem.Domain.CoursePeriodReviews;

namespace LearningPlatformSystem.API.CoursePeriods.AddCoursePeriodReview;

public sealed record AddCoursePeriodReviewRequest(Guid StudentId, Rating Rating, string? Comment);

