namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class SubcategoryEntity : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public string Name { get; set; } = null!;
}
