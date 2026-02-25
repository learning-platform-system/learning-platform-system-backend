using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Classrooms.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.Shared.Exceptions;

// Hanterar affärsregelbrott i exception och förväntade fel (Conflict, NotFound...) inte infrastructure- eller systemfel exceptions.

namespace LearningPlatformSystem.Application.Classrooms;
// I frontend välja i dropdown typ av klassrum sedan kommer alla klassrum av den typen i lista
public class ClassroomService(IClassroomRepository classroomRepository, IUnitOfWork unitOfWork) : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository = classroomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ApplicationResult<Guid>> CreateAsync(CreateClassroomInput input, CancellationToken ct)
    {
        if (await _classroomRepository.ExistsByNameAsync(input.Name, ct))
        {
            ApplicationResultError error = ClassroomApplicationErrors.NameAlreadyExists(input.Name);
            return ApplicationResult<Guid>.Fail(error);
        }

        Classroom classroom = Classroom.Create
        (
        Guid.NewGuid(),
        input.Name,
        input.Capacity,
        input.Type
        );

        await _classroomRepository.AddAsync(classroom, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult<Guid>.Success(classroom.Id);
    }

    public async Task<ApplicationResult> DeleteAsync(Guid id, CancellationToken ct)
    {
        bool exists = await _classroomRepository.RemoveAsync(id, ct);

        if (!exists)
        {
            ApplicationResultError error = ClassroomApplicationErrors.CouldNotBeFound(id);
            return ApplicationResult.Fail(error);
        }

        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }

    public async Task<ApplicationResult<IReadOnlyList<ClassroomOutput>>> GetByTypeAsync(ClassroomType type, CancellationToken ct)
    {
        IReadOnlyList<Classroom> classrooms = await _classroomRepository.GetByTypeAsync(type, ct);

        IReadOnlyList<ClassroomOutput> classroomOutputs = classrooms
            .Select(c => new ClassroomOutput(
                c.Id,
                c.Name,
                c.Capacity,
                c.Type))
            .ToList();

        return ApplicationResult<IReadOnlyList<ClassroomOutput>>.Success(classroomOutputs);
    }

    // 3 st anrop till datbasen???
    public async Task<ApplicationResult> UpdateAsync(UpdateClassroomInput input, CancellationToken ct)
    {
       Classroom? classroom = await _classroomRepository.GetByIdAsync(input.Id, ct);

        if (classroom is null)
        {
            ApplicationResultError error = ClassroomApplicationErrors.CouldNotBeFound(input.Id);
            return ApplicationResult.Fail(error);
        }

        if (await _classroomRepository.ExistsAnotherWithSameNameAsync(input.Name, input.Id, ct))
        {
            ApplicationResultError error = ClassroomApplicationErrors.NameAlreadyExists(input.Name);
            return ApplicationResult.Fail(error);
        }

        classroom.Update(input.Name, input.Capacity, input.Type);

        await _classroomRepository.UpdateAsync(classroom, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ApplicationResult.Success();
    }
}

/*
SaveChangesAsyncSkapa CoursePeriod, lägg till: sessions, resources etc.
Repot anropas vid varje action men ingenting sparas - flera actions buntas ihop i en transaktion, antingen lyckas alla eller misslyckas
*/


