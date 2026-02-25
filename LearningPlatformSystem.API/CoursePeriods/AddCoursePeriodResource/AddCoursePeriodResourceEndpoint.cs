using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.CoursePeriods.AddCoursePeriodResource;

public static class AddCoursePeriodResourceEndpoint
{
    public static RouteGroupBuilder MapPostCoursePeriodResourceEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{coursePeriodId:guid}/resources", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(Guid coursePeriodId, AddCoursePeriodResourceRequest request, ICoursePeriodService service, CancellationToken ct)
    {
     
        AddCoursePeriodResourceInput input = new(coursePeriodId, request.Title, request.Url, request.Description);
        ApplicationResult result = await service.AddResourceAsync(input, ct);

        if (!result.IsSuccess)
        {
            return result.ToHttpFailResult();
        }

        return Results.Created();
    }
}
