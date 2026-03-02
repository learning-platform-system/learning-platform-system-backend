using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;
using LearningPlatformSystem.Domain.Students;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Persistence.Repositories;

public class StudentRepository(LearningPlatformDbContext _context) : IStudentRepository
{
    public async Task AddAsync(Student aggregate, CancellationToken ct)
    {
        StudentEntity entity = new StudentEntity
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
        
        await _context.Students.AddAsync(entity, ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await _context.Students.AnyAsync(student => student.Id == id, ct);
    }

    public async Task<bool> ExistsWithTheSameEmailAsync(string email, CancellationToken ct)
    {
        return await _context.Students.AnyAsync(studentEntity => studentEntity.ContactInformation.Email == email, ct);
    }

    public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken ct)
    {
        IReadOnlyList<Student> students = await _context.Students
            .AsNoTracking()
            .Select(studentEntity => Student.Rehydrate(studentEntity.Id, studentEntity.Name, studentEntity.ContactInformation, studentEntity.Address))
            .ToListAsync(ct);  
        
        return students;
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        StudentEntity? entity = await _context.Students.SingleOrDefaultAsync(studentEntity => studentEntity.Id == id, ct);

        if (entity == null) return null;

        return Student.Rehydrate(entity.Id, entity.Name, entity.ContactInformation, entity.Address);
    }


    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        StudentEntity? studentEntity = await _context.Students.SingleOrDefaultAsync(studentEntity => studentEntity.Id == id, ct);
        
        if (studentEntity == null) return false;

        _context.Students.Remove(studentEntity);

        return true;
    }

    public async Task UpdateAsync(Student student, CancellationToken ct)
    {
        StudentEntity? studentEntity = await _context.Students.SingleAsync(studentEntity => studentEntity.Id == student.Id, ct);

        studentEntity.Address = student.Address;
    }
}
