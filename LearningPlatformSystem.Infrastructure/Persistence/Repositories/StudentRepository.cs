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
        StudentEntity studentEntity = new StudentEntity
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
        
        await _context.Students.AddAsync(studentEntity, ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await _context.Students.AnyAsync(student => student.Id == id, ct);
    }

    public async Task<bool> ExistsWithTheSameEmailAsync(string email, CancellationToken ct)
    {
        return await _context.Students.AnyAsync(studentEntity => studentEntity.ContactInformation.Email == email, ct);
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken ct)
    {
        StudentEntity? studentEntity = await _context.Students.SingleOrDefaultAsync(studentEntity => studentEntity.Id == id, ct);
        
        if (studentEntity == null) return false;

        _context.Students.Remove(studentEntity);

        return true;
    }
}
