using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Classrooms;

public interface IClassroomRepository : IRepositoryBase<Classroom, Guid>
{
    Task<IReadOnlyList<Classroom>> GetByTypeAsync(ClassroomType type, CancellationToken ct);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);
    Task<bool> ExistsAnotherWithSameNameAsync(string name, Guid classroomId, CancellationToken ct);
    Task UpdateAsync(Classroom classroom, CancellationToken ct);

}
