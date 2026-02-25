using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CourseEntityConfiguration : EntityBaseConfiguration<CourseEntity>
{
    public override void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Courses", table =>
        {
            table.HasCheckConstraint(
                "CK_Courses_Credits_Range", // CK = check constraint. CK_<Tabell>_<Kolumn>_<Vad den kontrollerar>
                $"[Credits] BETWEEN {Course.CreditsMinValue} AND {Course.CreditsMaxValue}");
        });

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(Course.CourseTitleMaxLength);

        builder.Property(e => e.Description)
            .HasMaxLength(Course.CourseDescriptionMaxLength);

        builder.Property(e => e.Credits)
            .IsRequired();

        builder.HasOne(e => e.Subcategory)
            .WithMany()
            .HasForeignKey(e => e.SubcategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index: 
        // Visa alla kurser inom en viss subkategori-sökning
        builder.HasIndex(e => e.SubcategoryId);

        // Kurstitel unika inom en subkategori (SubcategoryId + Title)
        builder.HasIndex(e => new { e.SubcategoryId, e.Title })
       .IsUnique();
    }
}
