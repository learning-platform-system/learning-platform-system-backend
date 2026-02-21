using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Shared.Exceptions;
using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Application.Classrooms;

public class ClassroomService(IClassroomRepository classroomRepository, IUnitOfWork unitOfWork) : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository = classroomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ApplicationResult> CreateAsync(CreateClassroomInput input, CancellationToken ct)
    {
        try
        {
            if (await classroomRepository.ExistsByNameAsync(input.Name, ct))
            {
                ApplicationResultError error = ClassroomApplicationErrors.NameAlreadyExists(input.Name);
                return ApplicationResult.Fail(error);
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

            return ApplicationResult.Ok();
        }
        catch (DomainException ex) 
        {
            ApplicationResultError error = ClassroomApplicationErrors.BadRequest(ex.Message);
            return ApplicationResult.Fail(error);
        }
        catch (PersistenceException ex) // wrappar DbUpdateException från SaveChangesAsync 
        {
            Exception? originalException = ex.InnerException;

            ApplicationResultError error = PersistenceErrors.SaveFailed(ex.Message);
            return ApplicationResult.Fail(error);
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

