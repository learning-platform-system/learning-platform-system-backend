using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Courses;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Courses.Delete;

public static class DeleteCourseEndpoint
{
    public static RouteGroupBuilder MapDeleteCourseEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, ICourseService service, CancellationToken ct)
    {
        ApplicationResult result = await service.DeleteCourseAsync(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.NoContent();
    }
}