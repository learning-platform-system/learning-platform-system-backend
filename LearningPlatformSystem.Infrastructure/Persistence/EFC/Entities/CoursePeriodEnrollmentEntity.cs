using LearningPlatformSystem.Domain.CoursePeriodEnrollments;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class CoursePeriodEnrollmentEntity : BaseEntity
{
    public Guid CoursePeriodId { get; set; }
    public CoursePeriodEntity? CoursePeriod { get; set; } = null;
    public Guid StudentId { get; set; }
    public StudentEntity? Student { get; set; } = null;
    public Grade Grade { get; set; }
}
