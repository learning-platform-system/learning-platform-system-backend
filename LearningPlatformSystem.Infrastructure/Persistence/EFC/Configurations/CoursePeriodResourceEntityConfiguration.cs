using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CoursePeriodResourceEntityConfiguration : EntityBaseConfiguration<CoursePeriodResourceEntity>
{
    public override void Configure(EntityTypeBuilder<CoursePeriodResourceEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("CoursePeriodResources");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Title)
           .IsRequired()
           .HasMaxLength(CoursePeriodResource.TitleMaxLength);

        builder.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(CoursePeriodResource.UrlMaxLength);

        builder.Property(e => e.Description)
            .HasMaxLength(CoursePeriodResource.DescriptionMaxLength);

        builder.HasOne(e => e.CoursePeriod)
            .WithMany(cp => cp.Resources)
            .HasForeignKey(e => e.CoursePeriodId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index:
        // hämta alla resources för kursperioden med id
        builder.HasIndex(e => e.CoursePeriodId);

    }
}
