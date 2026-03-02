using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers;

namespace LearningPlatformSystem.API.Teachers.Delete;

public static class DeleteTeacherEndpoint
{
    public static RouteGroupBuilder MapDeleteTeacherEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, ITeacherService service, CancellationToken ct)
    {
        ApplicationResult result = await service.DeleteTeacherAsync(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.NoContent();
    }
}
