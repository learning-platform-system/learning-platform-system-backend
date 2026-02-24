using LearningPlatformSystem.Application.Categories.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Application.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ApplicationResult<Guid>> CreateAsync(CreateCategoryInput input, CancellationToken ct)
    {
        bool exists = await _categoryRepository.ExistsByNameAsync(input.Name, ct);

        if (exists)
        {
            ApplicationResultError error = CategoryApplicationErrors.NameAlreadyExists(input.Name);
            return ApplicationResult<Guid>.Fail(error);
        }

        try
        {
            Category category = Category.Create(Guid.NewGuid(), input.Name);

            await _categoryRepository.AddAsync(category, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return ApplicationResult<Guid>.Success(category.Id);
        }
        catch (DomainException ex)
        {
            ApplicationResultError error = CategoryApplicationErrors.BadRequest(ex.Message);
            return ApplicationResult<Guid>.Fail(error);
        }
    }

    public async Task<ApplicationResult> DeleteAsync(Guid id, CancellationToken ct)
    {
        try
        {
            Category? category = await _categoryRepository.GetByIdAsync(id, ct);
            
            if (category == null)
            {
                ApplicationResultError error = CategoryApplicationErrors.NotFound(id);
                return ApplicationResult.Fail(error);
            }

            category.EnsureCanBeRemoved();

            await _categoryRepository.RemoveAsync(category.Id, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return ApplicationResult.Success();

        }
        catch (DomainException ex)
        {
            ApplicationResultError error = CategoryApplicationErrors.BadRequest(ex.Message);
            return ApplicationResult.Fail(error);
        }
    }
}
