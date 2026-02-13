namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CourseSessionEntity : EntityBase
{
    public Guid Id { get; set; }
    public Guid CoursePeriodId { get; set; }
    public CoursePeriodEntity CoursePeriod { get; set; } = null!;
    public Guid ClassroomId { get; set; }
    public ClassroomEntity Classroom { get; set; } = null!;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public ICollection<CourseSessionAttendanceEntity> Attendances { get; set; } = [];

}
/*
 * TimeOnly konfigurera i fluent API:
 builder.Property(e => e.Date)
       .HasColumnType("date");

builder.Property(e => e.StartTime)
       .HasColumnType("time");

builder.Property(e => e.EndTime)
       .HasColumnType("time");
 */