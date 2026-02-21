using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;
// IEntityTypeConfiguratios syfte är att möjliggöra databas-mappning av en entitet i en separat klass, annars måste man skriva allt i DbContext OnModelCreating. EF Core anropar automatiskt Configure när modellen byggs.
public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    // virtual = den här metoden får ändras (överskrivas) i en klass som ärver.
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedAt)
           .IsRequired()
           .HasColumnType("datetime2(0)")
           .HasDefaultValueSql("SYSUTCDATETIME()")
           .ValueGeneratedOnAdd();

        builder.Property(e => e.ModifiedAt)
            .IsRequired()
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}
/*
builder är ett EF Core-verktyg som används för att konfigurera hur en entitet mappas till databasen. 
Det är en del av Fluent API i Entity Framework Core, som ger mer kontroll över databasstrukturen och beteendet än vad som kan uppnås med dataanoteringar (Data Annotations) i entitetsklasserna. 
Genom att använda builder kan du specificera detaljer som datatyper, required, relationer mellan tabeller, maxläng, konverteringar, concurrency etc. vilket gör det möjligt att anpassa databasschemat efter egna behov.
*/