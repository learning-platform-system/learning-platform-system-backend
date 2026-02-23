using LearningPlatformSystem.Domain.Categories;
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

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Category aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
