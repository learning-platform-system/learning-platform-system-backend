using LearningPlatformSystem.Domain.CourseSessionAttendances;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CourseSessionAttendanceEntity : BaseEntity
{
    public Guid StudentId { get; set; }
    public StudentEntity Student { get; set; } = null!;
    public Guid CourseSessionId { get; set; }
    public CourseSessionEntity CourseSession { get; set; } = null!;
    public AttendanceStatus Status { get; set; }
}
