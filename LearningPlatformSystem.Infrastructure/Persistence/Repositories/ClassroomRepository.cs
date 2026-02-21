using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class ClassroomRepository(LearningPlatformDbContext context) : IClassroomRepository
{
    private readonly LearningPlatformDbContext _context = context;

    public async Task AddAsync(Classroom aggregate, CancellationToken ct)
    {
        ClassroomEntity classroomEntity = ToEntity(aggregate);

        await _context.Classrooms.AddAsync(classroomEntity, ct);
    }

    public async Task<IReadOnlyList<Classroom>> GetByTypeAsync(ClassroomType type, CancellationToken ct)
    {
       List<Classroom> classrooms = await _context.Classrooms
            .AsNoTracking()
            .Where(cEntity => cEntity.Type == type )
            .Select(cEntity => Classroom.Create(
                cEntity.Id,
                cEntity.Name,
                cEntity.Capacity,
                cEntity.Type))
            .ToListAsync(ct);

        return classrooms;
    }

    public async Task<Classroom?> GetByIdAsync(Guid id, CancellationToken ct)
    {
       Classroom? classroom = await _context.Classrooms
            .AsNoTracking()
            .Where(cEntity => cEntity.Id == id)
            .Select(cEntity => Classroom.Create(
                cEntity.Id,
                cEntity.Name,
                cEntity.Capacity,
                cEntity.Type))
            .SingleOrDefaultAsync(ct); // talar om att det bara gäller en, inte en lista

        return classroom;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        ClassroomEntity? entity = await _context.Classrooms.SingleOrDefaultAsync(cEntity => cEntity.Id == id, ct);   

        if (entity == null)
        {
            return false;
        }

        _context.Classrooms.Remove(entity);
        return true;
    }

    public async Task<bool> UpdateAsync(Classroom aggregate, CancellationToken ct)
    {
        ClassroomEntity? entity = await _context.Classrooms.SingleOrDefaultAsync(cEntity => cEntity.Id == aggregate.Id, ct);

        if (entity is null)
        {
            return false;
        }

        entity.Name = aggregate.Name;
        entity.Type = aggregate.Type;
        entity.Capacity = aggregate.Capacity;

        return true;
        // EFC trackar ändringarna på entity och kommer att uppdatera databasen när SaveChangesAsync anropas i UnitOfWork
    }

    private static ClassroomEntity ToEntity(Classroom aggregate)
    {
        ClassroomEntity classroomEntity = new()
        {
            Id = aggregate.Id,
            Name = aggregate.Name,
            Capacity = aggregate.Capacity,
            Type = aggregate.Type
        };

        return classroomEntity;
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct)
    {
       return await _context.Classrooms.AsNoTracking().AnyAsync(classroomEntity => classroomEntity.Name == name);
    }
}
