using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Subcategories;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CategoryRepository(LearningPlatformDbContext context) : ICategoryRepository
{
    private readonly LearningPlatformDbContext _context = context;

    public async Task AddAsync(Category category, CancellationToken ct)
    {
        CategoryEntity entity = new() { Name = category.Name, Id = category.Id };
        await _context.Categories.AddAsync(entity, ct);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct)
    {
        bool exists = await _context.Categories.AnyAsync(cEntity => cEntity.Name == name, ct);

        return exists;  
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        CategoryEntity? categoryEntity = await _context.Categories
            .AsNoTracking()
            .Include(e => e.Subcategories)
            .SingleOrDefaultAsync(e => e.Id == id, ct);

        if (categoryEntity == null)
            return null;

        IEnumerable<Subcategory> subcategories = categoryEntity.Subcategories
            .Select(sub => Subcategory.BuildFromDatabase(
                sub.Id,
                sub.CategoryId,
                sub.Name));

        return Category.BuildFromDatabase(categoryEntity.Id, categoryEntity.Name, subcategories);
    } 

    // category får inte tas bort om den har subcategories
    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        CategoryEntity? entity = await _context.Categories.SingleOrDefaultAsync(entity => entity.Id == id, ct);

        if (entity == null)
        {
            return false;
        }

        _context.Categories.Remove(entity);
        return true;
    }

    public Task<bool> UpdateAsync(Category aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
