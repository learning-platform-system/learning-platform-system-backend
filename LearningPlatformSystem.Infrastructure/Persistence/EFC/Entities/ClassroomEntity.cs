using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

public sealed class ClassroomEntity : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public ClassroomType Type { get; set; }
}
