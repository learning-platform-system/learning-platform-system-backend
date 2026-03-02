using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CourseSessionEntityConfiguration : EntityBaseConfiguration<CourseSessionEntity>
{
    public override void Configure(EntityTypeBuilder<CourseSessionEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("CourseSessions");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.HasOne(e => e.CoursePeriod)
            .WithMany(cp => cp.Sessions)
            .HasForeignKey(e => e.CoursePeriodId)
            .OnDelete(DeleteBehavior.Cascade); // tas CoursePeriod bort tas också dess CourseSessions bort

        builder.Property(e => e.Format)
            .IsRequired()
            .HasConversion<string>() // Konvertera enum till string i databasen
            .HasMaxLength(10); // maxlängd för string-representationen av enum, SQL Server gör annars nvarchar(max)

        builder.HasOne(e => e.Classroom)
            .WithMany()
            .HasForeignKey(e => e.ClassroomId)
            .OnDelete(DeleteBehavior.SetNull); // tas Classroom bort sätts ClassroomId i CourseSession till null

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.StartTime)
            .IsRequired();

        builder.Property(e => e.EndTime)
            .IsRequired();

        builder.HasMany(e => e.Attendances)
            .WithOne(a => a.CourseSession)
            .HasForeignKey(a => a.CourseSessionId)
            .OnDelete(DeleteBehavior.Cascade); // tas CourseSession bort tas också dess CourseSessionAttendances bort
    }
}