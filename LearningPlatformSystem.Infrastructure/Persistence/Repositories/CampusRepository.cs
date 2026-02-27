using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CampusRepository(LearningPlatformDbContext context) : ICampusRepository
{
    private readonly LearningPlatformDbContext _context = context;
    public async Task AddAsync(Campus aggregate, CancellationToken ct)
    {
        CampusEntity entity = new CampusEntity
        {
            Id = aggregate.Id,
            Name = aggregate.Name,
            Address = aggregate.Address,
        };

        await _context.Campuses.AddAsync(entity, ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await _context.Campuses.AnyAsync(campus => campus.Id == id, ct);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct)
    {
        return await _context.Campuses.AnyAsync(campus => campus.Name == name, ct);
    }

    public Task<Campus?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Campus aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
