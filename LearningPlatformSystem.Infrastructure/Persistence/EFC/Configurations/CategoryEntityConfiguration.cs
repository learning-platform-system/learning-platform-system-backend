using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CategoryEntityConfiguration : EntityBaseConfiguration<CategoryEntity>
{
    public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Categories");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(Category.CategoryNameMaxLength);

        builder.HasMany(e => e.Subcategories) // en Category har många Subcategories
            .WithOne(s => s.Category) // varje Subcategory har exakt en Category
            .HasForeignKey(s => s.CategoryId) // Subcategory äger Category FK (beroendet)
            .OnDelete(DeleteBehavior.Cascade); // när en Category tas bort, ta också bort alla Subcategories med det CategoryId

        // Index:
        builder.HasIndex(e => e.Name)
       .IsUnique();

    }
}
