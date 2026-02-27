using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;
using LearningPlatformSystem.Domain.Teachers;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class TeacherRepository(LearningPlatformDbContext context) : ITeacherRepository
{
    private readonly LearningPlatformDbContext _context = context;

    public async Task AddAsync(Teacher aggregate, CancellationToken ct)
    {
        TeacherEntity entity = new TeacherEntity
        {
            Id = aggregate.Id,
            Name = PersonName.Create
            (
                aggregate.Name.FirstName,
                aggregate.Name.LastName
            ),
            ContactInformation = ContactInformation.Create
            (
                aggregate.ContactInformation.Email,
                aggregate.ContactInformation.PhoneNumber
            ),
        };

        await _context.Teachers.AddAsync(entity, ct);
    }

    public async Task<bool> ExistsAsync(Guid teacherId, CancellationToken ct)
    {
        return await _context.Teachers.AnyAsync(teacher => teacher.Id == teacherId, ct);
    }

    public async Task<bool> ExistsWithTheSameEmailAsync(string email, CancellationToken ct)
    {
        return await _context.Teachers.AnyAsync(teacherEntity => teacherEntity.ContactInformation.Email == email, ct);
    }

    public async Task<IReadOnlyList<Teacher>> GetAllAsync(CancellationToken ct)
    {
        IReadOnlyList<Teacher> teachers = await _context.Teachers
            .AsNoTracking()
            .Select(teacherEntity => Teacher.Rehydrate(teacherEntity.Id, teacherEntity.Name, teacherEntity.ContactInformation, teacherEntity.Address))
            .ToListAsync(ct);

        return teachers;
    }

    public async Task<Teacher?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        TeacherEntity? entity = await _context.Teachers.SingleOrDefaultAsync(teacherEntity => teacherEntity.Id == id, ct);

        if (entity == null) return null;

        return Teacher.Rehydrate(entity.Id, entity.Name, entity.ContactInformation, entity.Address);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        TeacherEntity? entity = await _context.Teachers.SingleOrDefaultAsync(teacher => teacher.Id == id);

        if (entity == null) return false; 

        _context.Teachers.Remove(entity);
        return true;
    }

    public async Task UpdateAsync(Teacher teacher, CancellationToken ct)
    {
        TeacherEntity? entity = await _context.Teachers.SingleAsync(teacherEntity => teacherEntity.Id == teacher.Id, ct);                    

         entity.Address = teacher.Address;
    }
}
