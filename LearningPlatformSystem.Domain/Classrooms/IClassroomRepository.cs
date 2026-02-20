using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Classrooms;

public interface IClassroomRepository : IRepositoryBase<Classroom, Guid>
{
    Task<IReadOnlyList<Classroom>> GetClassroomByTypeAsync(ClassroomType type, CancellationToken ct);

}
