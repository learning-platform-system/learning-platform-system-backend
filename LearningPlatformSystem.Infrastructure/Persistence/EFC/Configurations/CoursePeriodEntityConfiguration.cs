using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Configurations;

public class CoursePeriodEntityConfiguration : EntityBaseConfiguration<CoursePeriodEntity>
{
    public override void Configure(EntityTypeBuilder<CoursePeriodEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("CoursePeriods");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.Property(e => e.Format)
            .IsRequired()
            .HasConversion<string>() // Konvertera enum till string i databasen
            .HasMaxLength(10); // maxlängd för string-representationen av enum, SQL Server gör annars nvarchar(max)

        builder.HasOne(e => e.Course)
            .WithMany(c => c.CoursePeriods)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // tas kursen bort ska alla dess kursperioder också tas bort

        builder.HasOne(e => e.Teacher)
            .WithMany() // courseperiod har bara beroende av läraren
            .HasForeignKey(e => e.TeacherId)
            .OnDelete(DeleteBehavior.Restrict); // tas läraren bort ska inte kursperioderna tas bort

        builder.HasOne(e => e.Campus)
            .WithMany() 
            .HasForeignKey(e => e.CampusId)
            .OnDelete(DeleteBehavior.SetNull); // tas campus bort ska campus-id i kursperioden sättas till null. Campus är nullable

        builder.HasMany(e => e.Sessions)
            .WithOne(cs => cs.CoursePeriod)
            .HasForeignKey(cs => cs.CoursePeriodId)
            .OnDelete(DeleteBehavior.Cascade); // tas kursperioden bort ska alla dess sessioner också tas bort

        builder.HasMany(e => e.Enrollments)
            .WithOne(ce => ce.CoursePeriod)
            .HasForeignKey(ce => ce.CoursePeriodId)
            .OnDelete(DeleteBehavior.Cascade); // tas kursperioden bort ska alla dess enrollments också tas bort

        builder.HasMany(e => e.Resources)
            .WithOne(cr => cr.CoursePeriod)
            .HasForeignKey(cr => cr.CoursePeriodId)
            .OnDelete(DeleteBehavior.Cascade); // tas kursperioden bort ska alla dess resurser också tas bort

        // Index:
        // sortera på startdatum
        builder.HasIndex(e => e.StartDate);
    }
}
