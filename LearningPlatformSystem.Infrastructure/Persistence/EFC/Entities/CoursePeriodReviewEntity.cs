using LearningPlatformSystem.Domain.CoursePeriodReviews;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CoursePeriodReviewEntity : EntityBase
{
    public Guid Id { get; set; }
    public Guid CoursePeriodId { get; set; }
    public Guid StudentId { get; set; }
    public Rating Rating { get; set; }
    public string? Comment { get; set; }
}
