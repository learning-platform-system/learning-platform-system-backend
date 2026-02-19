using LearningPlatformSystem.Domain.CoursePeriodReviews;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CoursePeriodReviewEntity : EntityBase
{
    public Guid Id { get; set; }
    public Guid CoursePeriodId { get; set; }
    public CoursePeriodEntity CoursePeriod { get; set; } = null!;
    public Guid StudentId { get; set; }
    public StudentEntity Student { get; set; } = null!;

    // konverteras till int i databasen
    public Rating Rating { get; set; } = null!;
    public string? Comment { get; set; }
}

