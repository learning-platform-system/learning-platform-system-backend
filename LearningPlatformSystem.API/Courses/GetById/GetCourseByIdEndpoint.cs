using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Courses;
using LearningPlatformSystem.Application.Courses.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Courses.GetById;

public static class GetCourseByIdEndpoint
{
    public static RouteGroupBuilder MapGetCourseByIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}", HandleAsync);

        return group;
    }
    private static async Task<IResult> HandleAsync(Guid id, ICourseService service, CancellationToken ct)
    {
        ApplicationResult<CourseOutput> result = await service.GetCourseById(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Ok(result.Data);
    }
}
