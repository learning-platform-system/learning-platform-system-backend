using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students.Inputs;
using LearningPlatformSystem.Domain.Students;

namespace LearningPlatformSystem.Application.Students;

// GetAll-metod
public class StudentService(IStudentRepository _studentRepository, IUnitOfWork _iUnitOfWork) : IStudentService
{
    public async Task<ApplicationResult> AddStudentAddressAsync(AddStudentAddressInput input, CancellationToken ct)
    {
        Student? student = await _studentRepository.GetByIdAsync(input.Id, ct);

        if (student is null)
            return ApplicationResult.Fail(StudentApplicationErrors.NotFound(input.Id));

        student.AddAddress(input.Street, input.PostalCode, input.City);

        await _studentRepository.UpdateAsync(student, ct);

        await _iUnitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult<Guid>> CreateStudentAsync(CreateStudentInput input, CancellationToken ct)
    {
        bool exists = await _studentRepository.ExistsWithTheSameEmailAsync(input.Email, ct);

        if (exists) 
            return ApplicationResult<Guid>.Fail(StudentApplicationErrors.EmailAlreadyExists(input.Email));

        Student student = Student.Create(input.FirstName, input.LastName, input.Email, input.PhoneNumber);

        await _studentRepository.AddAsync(student, ct);
        await _iUnitOfWork.SaveChangesAsync(ct);

        return ApplicationResult<Guid>.Success(student.Id);
    }

    public async Task<ApplicationResult> DeleteStudentAsync(Guid id, CancellationToken ct)
    {
        bool removed = await _studentRepository.RemoveAsync(id, ct);

        if (!removed)
        {
            return ApplicationResult.Fail(StudentApplicationErrors.NotFound(id));
        }

        await _iUnitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }
}
