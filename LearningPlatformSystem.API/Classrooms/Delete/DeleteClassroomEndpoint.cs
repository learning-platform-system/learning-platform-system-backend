using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Classrooms.Delete;

public static class DeleteClassroomEndpoint
{
    public static RouteGroupBuilder MapDeleteClassroomEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, IClassroomService service, CancellationToken ct)
    {
        ApplicationResult result = await service.DeleteClassroomAsync(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.NoContent();
    }
}
