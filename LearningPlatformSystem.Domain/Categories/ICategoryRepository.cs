
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Categories;

public interface ICategoryRepository : IRepositoryBase<Category, Guid>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);
    Task<bool> ExistsAnotherWithSameNameAsync(string name, Guid id, CancellationToken ct);
    Task<bool> SubcategoryExistsAsync(Guid id, CancellationToken ct);
    Task<bool> RemoveAsync(Guid id, CancellationToken ct);
    Task UpdateAsync(Category category, CancellationToken ct);

}
