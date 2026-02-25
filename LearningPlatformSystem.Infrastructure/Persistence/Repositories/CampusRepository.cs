using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CampusRepository(LearningPlatformDbContext context) : ICampusRepository
{
    private readonly LearningPlatformDbContext _context = context;
    public Task AddAsync(Campus aggregate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await _context.Campuses.AnyAsync(campus => campus.Id == id, ct);
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
