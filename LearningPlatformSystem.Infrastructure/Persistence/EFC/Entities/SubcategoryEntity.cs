namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class SubcategoryEntity : EntityBase
{
    public Guid Id { get; set; }
    // FK i kolumnen i databasen
    public Guid CategoryId { get; set; }
    // navigationproperty, används i koden för att navigera mellan entiteter (HasOne/WithMany i konfigureringen)
    public CategoryEntity Category { get; set; } = null!;
    public string Name { get; set; } = null!;
}
