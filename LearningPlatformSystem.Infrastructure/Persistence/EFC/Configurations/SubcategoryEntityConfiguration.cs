using LearningPlatformSystem.Domain.Subcategories;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class SubcategoryEntityConfiguration : EntityBaseConfiguration<SubcategoryEntity>
{
    public override void Configure(EntityTypeBuilder<SubcategoryEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Subcategories");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasColumnName("Name")
            .HasMaxLength(Subcategory.SubcategoryNameMaxLength)
            .IsRequired();

        builder.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Borttagning av Category blockeras om det finns Subcategories som refererar till den.

        // Index:
        // unikt namn inom den specifika kategorin (CategoryId + Name)
        builder.HasIndex(e => new { e.CategoryId, e.Name })
            .IsUnique();
    }
}