using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Shared.Exceptions;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC;
public class LearningPlatformDbContext : DbContext, IUnitOfWork
{
    public LearningPlatformDbContext(DbContextOptions<LearningPlatformDbContext> options) : base(options)
    {
    }

    // Returnerar DbSet (= en collection av entiteter) för ClassroomEntity via DbContext.Set<T>(), vilket representerar tabellen i databasen.
    public DbSet<ClassroomEntity> Classrooms => Set<ClassroomEntity>();
    public DbSet<CourseEntity> Courses => Set<CourseEntity>();
    public DbSet<CoursePeriodEnrollmentEntity> CoursePeriodEnrollments => Set<CoursePeriodEnrollmentEntity>();
    public DbSet<CoursePeriodEntity> CoursePeriods => Set<CoursePeriodEntity>();
    public DbSet<CourseSessionAttendanceEntity> CourseSessionAttendances => Set<CourseSessionAttendanceEntity>();
    public DbSet<CoursePeriodReviewEntity> CoursePeriodReviews => Set<CoursePeriodReviewEntity>();
    public DbSet<CoursePeriodResourceEntity> CoursePeriodResources => Set<CoursePeriodResourceEntity>();
    public DbSet<CampusEntity> Campuses => Set<CampusEntity>();
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<TeacherEntity> Teachers => Set<TeacherEntity>();
    public DbSet<SubcategoryEntity> Subcategories => Set<SubcategoryEntity>();
    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
    public DbSet<CourseSessionEntity> CourseSessions => Set<CourseSessionEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // EF Core letar efter ALLA klasser i assemblyn (projektet) som implementerar IEntityTypeConfiguration och anropar deras Configure-metod automatiskt.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LearningPlatformDbContext).Assembly);

        // SQLite har inte stöd för SYSUTCDATETIME() som standardvärde i SQL, måste ersätta det med CURRENT_TIMESTAMP för att säkerställa kompatibilitet med SQLite som databas. 
        if (Database.IsSqlite())
        {
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType
                     in modelBuilder.Model.GetEntityTypes())
            {
                foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableProperty property
                         in entityType.GetProperties())
                {
                    string? defaultSql = property.GetDefaultValueSql();

                    if (defaultSql == "SYSUTCDATETIME()")
                    {
                        property.SetDefaultValueSql("CURRENT_TIMESTAMP");
                    }
                }
            }
        }
    }

    // Överlagrar SaveChangesAsync för att fånga DbUpdateException och kasta en PersistenceException som kan hanteras i applikationslagret.
    public override async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        // hämtar aktuell tid 
        DateTime utcNow = DateTime.UtcNow;

        // hämtar alla entiteter som ärver EntityBase som EF spårar i den aktuella DbContext-instansen och sätter timestamps.
        foreach (EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
        {
            // kontrollera om entitetens tillstånd är Added eller Modified
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
                entry.Entity.ModifiedAt = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedAt = utcNow;
            }
        }

        try 
        {
            return await base.SaveChangesAsync(ct);
        }

        catch (DbUpdateException ex)
        {
            throw new PersistenceException("Ett fel uppstod vid sparning.", ex);
        }
    }
}

/*
När DbContext startar:
→ EF bygger modellen
→ EF anropar OnModelCreating
→ EF hittar dina IEntityTypeConfiguration-klasser
→ EF anropar Configure(builder)
*/