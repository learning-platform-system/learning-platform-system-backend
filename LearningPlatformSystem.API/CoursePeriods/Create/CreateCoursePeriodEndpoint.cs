using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Shared.Enums;

namespace LearningPlatformSystem.API.CoursePeriods.Create;

public static class CreateCoursePeriodEndpoint
{
    public static RouteGroupBuilder MapPostCoursePeriodEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(CreateCoursePeriodRequest request, ICoursePeriodService service, CancellationToken ct)
    {
        if (!Enum.TryParse(request.Format, true, out CourseFormat courseFormat))
        {
            return Results.BadRequest("Ogiltigt kursformat.");
        }

        CreateCoursePeriodInput input = new(request.CourseId, request.TeacherId, request.CampusId, request.StartDate, request.EndDate, courseFormat);
        ApplicationResult<Guid> result = await service.CreateCoursePeriodAsync(input, ct); 

        if(result.IsFailure)
        {
            result.ToHttpFailResult();
        }

        return Results.Created($"/coursePeriods/{result.Data}", new { id = result.Data });
    }
}
