using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Classrooms.Create;

public static class CreateClassroomEndpoint
{
    public static RouteGroupBuilder MapCreateClassroomEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", HandleAsync);

        return group;

    }

    private static async Task<IResult> HandleAsync(CreateClassroomRequest request, IClassroomService service, CancellationToken ct)
    {
        ApplicationResult<Guid> result = await service.CreateAsync(new CreateClassroomInput(request.Name, request.Capacity, request.Type), ct);

        if(!result.IsSuccess)
        {
            return result.ToHttpFailResult();
        }

        // Results.Created(string location, object? value). location =  URL:en till den nyskapade Classroom i endpointen GetById. Value är det som skickas tillbaka i response body
        return Results.Created($"/classrooms/{result.Data}", new { id = result.Data });
    }
}
