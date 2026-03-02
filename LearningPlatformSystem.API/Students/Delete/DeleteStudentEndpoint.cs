using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students;

namespace LearningPlatformSystem.API.Students.Delete;

public static class DeleteStudentEndpoint
{
    public static RouteGroupBuilder MapDeleteStudentEndpoint(this RouteGroupBuilder group)
    {
       group.MapDelete("{id:guid}", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, IStudentService service, CancellationToken ct)
    {
        ApplicationResult result = await service.DeleteStudentAsync(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.NoContent();
    }
}   
