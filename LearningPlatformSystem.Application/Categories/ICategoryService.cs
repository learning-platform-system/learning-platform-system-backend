using LearningPlatformSystem.Application.Categories.Inputs;
using LearningPlatformSystem.Application.Categories.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Categories;

public interface ICategoryService
{
    Task<ApplicationResult<Guid>> CreateAsync(CreateCategoryInput input, CancellationToken ct);

    Task<ApplicationResult<CategoryOutput>> GetByIdAsync(Guid id, CancellationToken ct);

    //Task<ApplicationResult> UpdateNameAsync(UpdateCategoryNameInput input, CancellationToken ct);

    Task<ApplicationResult> DeleteAsync(Guid id, CancellationToken ct);
}
