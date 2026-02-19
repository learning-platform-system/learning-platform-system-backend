using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CoursePeriodEnrollmentEntityConfiguration : EntityBaseConfiguration<CoursePeriodEnrollmentEntity>
{
    public override void Configure(EntityTypeBuilder<CoursePeriodEnrollmentEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("CoursePeriodEnrollments");

        builder.HasKey(e => new { e.StudentId, e.CoursePeriodId });

        builder.HasOne(e => e.Student)
         .WithMany() // student har ingen navigationsproperty för CoursePeriodEnrollmentEntity
         .HasForeignKey(e => e.StudentId)
         .OnDelete(DeleteBehavior.Cascade); // om en student tas bort, ta bort alla deras CoursePeriodEnrollmentEntity

        builder.HasOne(e => e.CoursePeriod)
           .WithMany(cp => cp.Enrollments)
           .HasForeignKey(e => e.CoursePeriodId)
           .OnDelete(DeleteBehavior.Cascade); // om en CoursePeriod tas bort, ta bort alla relaterade CoursePeriodEnrollmentEntity

        builder.Property(e => e.Grade)
            .IsRequired()
            .HasConversion<string>() // Konvertera enum till string i databasen
            .HasMaxLength(10); // maxlängd för string-representationen av enum, SQL Server gör annars nvarchar(max)

    }
}
