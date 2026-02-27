using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Campuses;
using LearningPlatformSystem.Application.Campuses.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Campuses.GetAll;

public static class GetAllCampusesEndpoint
{
    public static RouteGroupBuilder MapGetAllCampusesEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(ICampusService service, CancellationToken ct)
    {
        ApplicationResult<IReadOnlyList<CampusOutput>> result = await service.GetAllCampusesAsync(ct);

        if(result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.Ok(result.Data);
    }
}
