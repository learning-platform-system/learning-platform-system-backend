using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Outputs;
using LearningPlatformSystem.Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformSystem.API.CoursePeriods.GetByCourseId;

public static class GetByCourseIdEndpoint
{
    public static RouteGroupBuilder MapGetCoursePeriodsByCourseIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", HandleAsync);
        return group;
    }
    private static async Task<IResult> HandleAsync([FromBody]Guid courseId, ICoursePeriodService service, CancellationToken ct)
    {
        ApplicationResult<IReadOnlyList<CoursePeriodOutput>> result = await service.GetCoursePeriodByCourseIdAsync(courseId, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Ok(result.Data);
    }
}
