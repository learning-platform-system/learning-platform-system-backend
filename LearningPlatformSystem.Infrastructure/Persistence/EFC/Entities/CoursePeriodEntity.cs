using LearningPlatformSystem.Domain.CoursePeriods;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CoursePeriodEntity : EntityBase
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public CourseEntity Course { get; set; } = null!;
    public Guid TeacherId { get; set; }
    public TeacherEntity Teacher { get; set; } = null!;
    public Guid? CampusId { get; set; }
    public CampusEntity? Campus { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public CourseFormat Format { get; set; }
    public ICollection<CourseSessionEntity> Sessions { get; set; } = [];

    public ICollection<CoursePeriodEnrollmentEntity> Enrollments { get; set; } = [];

}

/* DateOnly konfiguration i fluent api:builder.Property(e => e.StartDate)
       .HasColumnType("date");

builder.Property(e => e.EndDate)
       .HasColumnType("date");
*/