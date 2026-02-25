using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Courses;
using LearningPlatformSystem.Application.Courses.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Courses.Create;

public static class CreateCourseEndpoint
{
    public static RouteHandlerBuilder MapPostCourseEndpoint(this RouteGroupBuilder group)
    {
        return group.MapPost("", HandleAsync);
    }

    private static async Task<IResult> HandleAsync(CreateCourseRequest request, ICourseService service, CancellationToken ct)
    {
        CreateCourseInput input = new
        (
            SubcategoryId: request.SubcategoryId,
            Title: request.Title,
            Description: request.Description,
            Credits: request.Credits
        );

        ApplicationResult<Guid> result = await service.CreateAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.Created($"/course/{result.Data}", new { id = result.Data });
    }
}