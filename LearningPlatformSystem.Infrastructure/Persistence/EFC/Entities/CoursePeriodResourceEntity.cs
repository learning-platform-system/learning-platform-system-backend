namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CoursePeriodResourceEntity : EntityBase
{
    public Guid Id { get; set; }
    public Guid CoursePeriodId { get; set; }
    public CoursePeriodEntity CoursePeriod { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
}
