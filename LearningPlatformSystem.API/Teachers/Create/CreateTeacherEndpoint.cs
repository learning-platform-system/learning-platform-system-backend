using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers;
using LearningPlatformSystem.Application.Teachers.Inputs;

namespace LearningPlatformSystem.API.Teachers.Create;

public static class CreateTeacherEndpoint
{
    public static RouteGroupBuilder MapPostTeacherEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(CreateTeacherRequest request, ITeacherService service, CancellationToken ct)
    {
        CreateTeacherInput input = new(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

        ApplicationResult<Guid> result = await service.CreateTeacherAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Created($"/teachers/{result.Data}", new { id = result.Data });
    }
}
