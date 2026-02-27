using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students;
using LearningPlatformSystem.Application.Students.Outputs;

namespace LearningPlatformSystem.API.Students.GetAll;

public static class GetAllStudentsEndpoint
{
    public static RouteGroupBuilder MapGetAllStudentsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(IStudentService service, CancellationToken ct)
    {
        ApplicationResult<IReadOnlyList<StudentOutput>> result = await service.GetAllStudentsAsync(ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.Ok(result.Data);
    }
}