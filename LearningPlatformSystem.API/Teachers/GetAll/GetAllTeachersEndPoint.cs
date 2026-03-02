using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers;
using LearningPlatformSystem.Application.Teachers.Outputs;

namespace LearningPlatformSystem.API.Teachers.GetAll;

public static class GetAllTeachersEndPoint
{
    public static RouteGroupBuilder MapGetAllTeachersEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", HandleAsync);
        return group;
    }
    private static async Task<IResult> HandleAsync(ITeacherService service, CancellationToken ct)
    {
        ApplicationResult<IReadOnlyList<TeacherOutput>> result = await service.GetAllTeachersAsync(ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Ok(result.Data);
    }
}
