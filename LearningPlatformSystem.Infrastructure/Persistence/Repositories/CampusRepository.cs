using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.Students;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class CampusRepository(LearningPlatformDbContext _context) : ICampusRepository
{
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

    public async Task<IReadOnlyList<Campus>> GetAllAsync(CancellationToken ct)
    {
        IReadOnlyList<Campus> campuses = await _context.Campuses
            .AsNoTracking()
            .Select(entity => Campus.Rehydrate(entity.Id, entity.Name, entity.Address, entity.ContactInformation))
            .ToListAsync(ct);

        return campuses;
    }

    public async Task<Campus?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        CampusEntity? entity = await _context.Campuses
            .AsNoTracking()
            .SingleOrDefaultAsync(campus => campus.Id == id, ct);

        if (entity is null) return null;

        return Campus.Rehydrate(entity.Id, entity.Name, entity.Address, entity.ContactInformation);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        CampusEntity? entity = await _context.Campuses.SingleOrDefaultAsync(campus => campus.Id == id, ct);

        if (entity is null) return false;

        _context.Campuses.Remove(entity);
        return true;
    }

    public async Task UpdateAsync(Campus aggregate, CancellationToken ct)
    {
        CampusEntity? entity = await _context.Campuses.SingleAsync(campus => campus.Id == aggregate.Id, ct);

        entity.ContactInformation = aggregate.ContactInformation;
    }
}
