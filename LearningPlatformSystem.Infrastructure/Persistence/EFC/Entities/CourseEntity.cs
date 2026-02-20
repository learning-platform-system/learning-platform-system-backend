using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CourseEntity : EntityBase
{
    public Guid Id { get; set; }
    public Guid SubcategoryId { get; set; }
    public SubcategoryEntity Subcategory { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Credits { get; set; }
    public ICollection<CoursePeriodEntity> CoursePeriods { get; set; } = [];

}
