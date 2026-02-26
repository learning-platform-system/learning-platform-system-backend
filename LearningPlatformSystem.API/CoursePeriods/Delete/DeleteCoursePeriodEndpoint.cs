using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.CoursePeriods.Delete;

public static class DeleteCoursePeriodEndpoint
{
    public static RouteGroupBuilder MapDeleteCoursePeriodEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", HandleAsync);
        return group;
    }
    private static async Task<IResult> HandleAsync(Guid id, ICoursePeriodService service, CancellationToken ct)
    {
        ApplicationResult result = await service.DeleteCoursePeriodAsync(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.NoContent();
    }
}
