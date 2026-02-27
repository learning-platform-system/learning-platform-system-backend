using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers.Inputs;
using LearningPlatformSystem.Application.Teachers.Outputs;
using LearningPlatformSystem.Domain.Teachers;

namespace LearningPlatformSystem.Application.Teachers;

public class TeacherService(ITeacherRepository _teacherRepository, IUnitOfWork _iUnitOfWork) : ITeacherService
{
    public async Task<ApplicationResult> AddTeacherAddressAsync(AddTeacherAddressInput input, CancellationToken ct)
    {
        Teacher? teacher = await _teacherRepository.GetByIdAsync(input.Id, ct);

        if (teacher is null)
            return ApplicationResult.Fail(TeacherApplicationErrors.NotFound(input.Id));
       
        teacher.AddAddress(input.Street, input.PostalCode, input.City);

        await _teacherRepository.UpdateAsync(teacher, ct);
        await _iUnitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult<Guid>> CreateTeacherAsync(CreateTeacherInput input, CancellationToken ct)
    {
        bool exists = await _teacherRepository.ExistsWithTheSameEmailAsync(input.Email, ct);

        if (exists)
        {
            return ApplicationResult<Guid>.Fail(TeacherApplicationErrors.EmailAlreadyExists(input.Email));
        }

        Teacher teacher = Teacher.Create(input.FirstName, input.LastName, input.Email, input.PhoneNumber);

        await _teacherRepository.AddAsync(teacher, ct);
        await _iUnitOfWork.SaveChangesAsync(ct);

        return ApplicationResult<Guid>.Success(teacher.Id);
    }

    public async Task<ApplicationResult> DeleteTeacherAsync(Guid id, CancellationToken ct)
    {
        bool removed = await _teacherRepository.RemoveAsync(id, ct);

        if (!removed)
        {
            return ApplicationResult.Fail(TeacherApplicationErrors.NotFound(id));
        }

        await _iUnitOfWork.SaveChangesAsync(ct);
        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult<IReadOnlyList<TeacherOutput>>> GetAllTeachersAsync(CancellationToken ct)
    {
        IReadOnlyList<Teacher> teachers = await _teacherRepository.GetAllAsync(ct);

        IReadOnlyList<TeacherOutput> teacherOutputs = teachers.Select(teacher => new TeacherOutput
        (
            Id: teacher.Id,
            FirstName: teacher.Name.FirstName,
            LastName: teacher.Name.LastName,
            Email: teacher.ContactInformation.Email,
            PhoneNumber: teacher.ContactInformation.PhoneNumber
        )).ToList();

        return ApplicationResult<IReadOnlyList<TeacherOutput>>.Success(teacherOutputs);
    }
}
