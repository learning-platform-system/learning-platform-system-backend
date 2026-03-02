using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CourseSessionAttendanceEntityConfiguration : EntityBaseConfiguration<CourseSessionAttendanceEntity>
{
    public override void Configure(EntityTypeBuilder<CourseSessionAttendanceEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("CourseSessionAttendances");

        builder.HasKey(e => new { e.StudentId, e.CourseSessionId });

        builder.HasOne(e => e.Student)
            .WithMany()
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(e => e.CourseSession)
            .WithMany(cs => cs.Attendances)
            .HasForeignKey(e => e.CourseSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Status)
            .HasConversion<string>() // Konvertera enum till string i databasen
            .HasMaxLength(20) // maxlängd för string-representationen av enum, SQL Server gör annars nvarchar(max)
            .IsRequired();
    }
}
