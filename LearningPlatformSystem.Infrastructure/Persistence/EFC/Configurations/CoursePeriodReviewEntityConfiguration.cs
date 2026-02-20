using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CoursePeriodReviewEntityConfiguration : EntityBaseConfiguration<CoursePeriodReviewEntity>
{
    public override void Configure(EntityTypeBuilder<CoursePeriodReviewEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("CoursePeriodReviews");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(r => r.Rating)
            .HasConversion(
                rating => rating.Value,        // Till databasen (Rating -> int)
                value => Rating.Create(value)) // Från databasen (int -> Rating)
            .IsRequired();

        builder.Property(e => e.Comment)
            .HasMaxLength(CoursePeriodReview.CommentMaxLength);

        builder.HasOne(e => e.CoursePeriod)
            .WithMany(cp => cp.Reviews)
            .HasForeignKey(e => e.CoursePeriodId)
            .OnDelete(DeleteBehavior.Cascade) // Om CoursePeriod tas bort ska dess reviews också tas bort
            .IsRequired();

        builder.HasOne(e => e.Student)
            .WithMany() // ingen navigation från student till review, tillhör olika aggregate.
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict) // Om Student tas bort ska dess reviews inte tas bort
            .IsRequired();

        // Index:
        // -bara en review per student per kursperiod, så en unik index på CoursePeriodId + StudentId
        builder.HasIndex(e => new { e.CoursePeriodId, e.StudentId })
       .IsUnique();
    }
}
