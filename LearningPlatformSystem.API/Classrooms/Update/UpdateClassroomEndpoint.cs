using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;

namespace LearningPlatformSystem.API.Classrooms.Update;

public static class UpdateClassroomEndpoint
{
    public static RouteGroupBuilder MapPutClassroomEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("", HandleAsync).WithName("UpdateClassroom");

        return group;
    }

    private static async Task<IResult> HandleAsync(UpdateClassroomRequest request, IClassroomService service, CancellationToken ct)
    {
        if (!Enum.TryParse(request.Type, true, out ClassroomType parsedType))
        {
            return Results.BadRequest("Ogiltig klassrumstyp.");
        }

        UpdateClassroomInput input = new
            (
            Id: request.Id,
            Name: request.Name,
            Capacity: request.Capacity,
            Type: parsedType
            );

        ApplicationResult result = await service.UpdateAsync(input, ct);

        if (!result.IsSuccess)
        {
            return result.ToHttpFailResult();
        }

        return Results.NoContent();
    }
}

