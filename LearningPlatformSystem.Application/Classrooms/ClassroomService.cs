using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Classrooms.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Shared.Exceptions;
using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.Shared.Exceptions;

// Hanterar affärsregelbrott i exception och förväntade fel (SaveFailed, Conflict...) inte infrastructure- eller systemfel exceptions.

namespace LearningPlatformSystem.Application.Classrooms;
// I frontend välja i dropdown typ av klassrum sedan kommer alla klassrum av den typen i lista
public class ClassroomService(IClassroomRepository classroomRepository, IUnitOfWork unitOfWork) : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository = classroomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ApplicationResult<ClassroomOutput>> CreateAsync(CreateClassroomInput input, CancellationToken ct)
    {
        if (await _classroomRepository.ExistsByNameAsync(input.Name, ct))
        {
            ApplicationResultError error = ClassroomApplicationErrors.NameAlreadyExists(input.Name);
            return ApplicationResult<ClassroomOutput>.Fail(error);
        }

        try
        {
            Classroom classroom = Classroom.Create
            (
            Guid.NewGuid(),
            input.Name,
            input.Capacity,
            input.Type
            );

            await _classroomRepository.AddAsync(classroom, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            ClassroomOutput classroomOutput = new ClassroomOutput(
            classroom.Id,
            classroom.Name,
            classroom.Capacity,
            classroom.Type);

            return ApplicationResult<ClassroomOutput>.Success(classroomOutput);
        }
        catch (DomainException ex) 
        {
            ApplicationResultError error = ClassroomApplicationErrors.BadRequest(ex.Message);
            return ApplicationResult<ClassroomOutput>.Fail(error);
        }
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
}

/*
DbUpdateException kastas när:
Unique constraint bryts
Foreign key bryts
NOT NULL bryts
Check constraint bryts
*/

/*
Skapa CoursePeriod, lägg till: sessions, resources etc.
Repot anropas vid varje action men ingenting sparas - flera actions buntas ihop i en transaktion, antingen lyckas alla eller misslyckas
*/ 

