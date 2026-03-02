using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Campuses;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Campuses.Delete;

public static class DeleteCampusEndpoint
{
    public static RouteGroupBuilder MapDeleteCampusEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}", HandleAsync);
        return group;
    }

  private static async Task<IResult> HandleAsync(Guid id, ICampusService service, CancellationToken ct)
    {
        ApplicationResult result = await service.DeleteCampusAsync(id, ct);
            
        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.NoContent();
    }
}