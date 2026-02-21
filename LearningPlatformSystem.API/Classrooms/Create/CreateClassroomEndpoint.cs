using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Classrooms.Create;

public static class CreateClassroomEndpoint
{
    public static IEndpointRouteBuilder MapCreateClassroomEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Classrooms").WithTags("Classroom");

        group.MapPost("", HandleAsync).WithName("CreateClassroom");

        return app;

    }

    private static async Task<IResult> HandleAsync(CreateClassroomRequest request, IClassroomService service)
    {
        ApplicationResult result = await service.CreateAsync(new CreateClassroomInput(request.Name, request.Capacity, request.Type), CancellationToken.None);

        // Func<IResult> onSuccess = () => Results.Created();
        // Results.Created(string location, object? value). location =  URL:en till den nyskapade Classroom i endpointen GetById. Value är det som skickas tillbaka i response body
        return result.ToHttpResult(() => Results.Created());
    }


}
