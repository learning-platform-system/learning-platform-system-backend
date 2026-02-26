using LearningPlatformSystem.Application.Categories.Inputs;
using LearningPlatformSystem.Application.Categories.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Categories;

public interface ICategoryService
{
    Task<ApplicationResult<Guid>> CreateCategoryAsync(CreateCategoryInput input, CancellationToken ct);

    Task<ApplicationResult<CategoryOutput>> GetCategoryByIdAsync(Guid id, CancellationToken ct);

    Task<ApplicationResult> UpdateCategoryNameAsync(UpdateCategoryNameInput input, CancellationToken ct);

    Task<ApplicationResult> DeleteCategoryAsync(Guid id, CancellationToken ct);
    Task<ApplicationResult> AddSubcategoryAsync(AddSubcategoryInput input, CancellationToken ct);
}
