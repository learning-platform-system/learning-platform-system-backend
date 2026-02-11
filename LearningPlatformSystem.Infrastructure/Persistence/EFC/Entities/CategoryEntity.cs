namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CategoryEntity : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    // lagras inte som en lista, EF mappar den till en relation (foreign key) / tabellkoppling)
    public ICollection<SubcategoryEntity> Subcategories { get; set; } = [];
}
