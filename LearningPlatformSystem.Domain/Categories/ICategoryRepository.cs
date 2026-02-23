
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Categories;

public interface ICategoryRepository : IRepositoryBase<Category, Guid>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);
}
