using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC;
public class LearningPlatformDbContext : DbContext
{
    public LearningPlatformDbContext(DbContextOptions<LearningPlatformDbContext> options) : base(options)
    {
    }
    // tabellen för ClassroomEntity hämtas från DbContext via Set<T>().
    public DbSet<ClassroomEntity> Classrooms => Set<ClassroomEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // EF Core letar efter ALLA klasser i assemblyn (projektet) som implementerar IEntityTypeConfiguration och anropar deras Configure-metod automatiskt.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LearningPlatformDbContext).Assembly);
    }
}

/*
När DbContext startar:
→ EF bygger modellen
→ EF anropar OnModelCreating
→ EF hittar dina IEntityTypeConfiguration-klasser
→ EF anropar Configure(builder)
*/