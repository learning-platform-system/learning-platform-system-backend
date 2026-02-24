using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class ClassroomEntityConfiguration
    : EntityBaseConfiguration<ClassroomEntity>
{
    // override = kan ändra beteendet från basklassen som är virtual, lägga till klasspecifik konfiguration
    public override void Configure(EntityTypeBuilder<ClassroomEntity> builder)
    {
        base.Configure(builder); // kör baskonfigurationen först för att få med de gemensamma konfigurationerna (EntityBaseConfiguration)

        builder.ToTable("Classrooms");
        // konfigurerar id som PK. Innebär i SQL Server, NOT NULL (varje rad måste ha ett id), unique constraint (regel om inga dubletter) och automatiskt unikt index (databasstruktur för snabb sökning)
        builder.HasKey(e => e.Id);

        //Guid-id sätts i domain, EF ska inte generera det.
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(Classroom.NameMaxLength);

        builder.Property(e => e.Capacity)
            .IsRequired();

        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<string>() // Konvertera enum till string i databasen
            .HasMaxLength(20); // maxlängd för string-representationen av enum, SQL Server gör annars nvarchar(max)

        // Index:
        builder.HasIndex(e => e.Name)
       .IsUnique();
    }
}
