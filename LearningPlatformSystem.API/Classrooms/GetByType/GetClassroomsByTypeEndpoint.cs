using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.Classrooms.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.API.Classrooms.GetByType;

public static class GetClassroomsByTypeEndpoint
{
    public static RouteGroupBuilder MapGetClassroomsByTypeEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", HandleAsync).WithName("GetClassroomsByType");

        return group;
    }

    private static async Task<IResult> HandleAsync(string type, IClassroomService service, CancellationToken ct)
    {
        if (!Enum.TryParse(type, true, out ClassroomType parsedType))
        {
            return Results.BadRequest("Ogiltig klassrumstyp.");
        }


        ApplicationResult<IReadOnlyList<ClassroomOutput>> result = await service.GetByTypeAsync(parsedType, ct);

        if (!result.IsSuccess)
        {
            return result.ToHttpFailResult();
        }

        return Results.Ok(result.Data);
    }
}
