namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CategoryEntity : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    // navigationproperty: representerar 1-* relationen till SubcategoryEntity. Används av EF Core för att konfigurera och spåra relationen (HasMany/WithOne). Möjliggör användning av Include() i Categoryrepot. 
    public ICollection<SubcategoryEntity> Subcategories { get; set; } = [];
}
