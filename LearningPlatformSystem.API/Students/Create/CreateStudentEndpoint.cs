using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students;
using LearningPlatformSystem.Application.Students.Inputs;

namespace LearningPlatformSystem.API.Students.Create;

public static class CreateStudentEndpoint
{
    public static RouteGroupBuilder MapPostStudentEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(CreateStudentRequest request, IStudentService service, CancellationToken ct)
    {
        CreateStudentInput input = new(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

        ApplicationResult<Guid> result = await service.CreateStudentAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.Created($"/students/{result.Data}", result.Data);
    }
}
