namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public abstract class EntityBase
{
    // Auditing-teknisk spårning, fälten används för att spåra när en adress skapades och senast ändrades. 
    public DateTime CreatedAt { get; set; } 
    public DateTime ModifiedAt { get; set; }

    // Concurrency, förhindrar krockande uppdateringar
    public byte[]? RowVersion { get; set; } = null!;
}
