using LearningPlatformSystem.Domain.Shared.Enums;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CourseSessionEntity : EntityBase
{
    public Guid Id { get; set; }
    public Guid CoursePeriodId { get; set; }
    public CoursePeriodEntity CoursePeriod { get; set; } = null!;
    public CourseFormat Format { get; set; }
    public Guid? ClassroomId { get; set; }
    public ClassroomEntity? Classroom { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public ICollection<CourseSessionAttendanceEntity> Attendances { get; set; } = [];

}
