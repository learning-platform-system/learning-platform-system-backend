using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Shared.Errors;
using LearningPlatformSystem.Application.Shared.Exceptions;
using LearningPlatformSystem.Application.Shared.Results;
using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Application.Classrooms;

public class ClassroomService(IClassroomRepository classroomRepository, IUnitOfWork unitOfWork) : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository = classroomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> CreateClassroomAsync(CreateClassroomInput input, CancellationToken ct)
    {
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

            return Result.Ok();
        }
        catch (DomainException ex) 
        {
            return Result.Fail(ClassroomApplicationErrors.BadRequest(ex.Message));
        }
        catch (PersistenceException ex) // från SaveChangesAsync 
        {
            Exception? originalException = ex.InnerException;

            return Result.Fail(PersistenceErrors.SaveFailed(ex.Message));
        }
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

