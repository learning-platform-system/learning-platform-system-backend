using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Courses;
using LearningPlatformSystem.Application.Courses.Inputs;
using LearningPlatformSystem.Application.Courses.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Courses.SearchCourses;

public static class SearchCoursesEndpoint
{
    public static RouteGroupBuilder MapSearchCoursesEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("search", HandleAsync);
        return group;
    }

    // [AsParameters] gör att query string-parametrar i URL:en binds till motsvarande properties i SearchCoursesRequest baserat på namn.
    private static async Task<IResult> HandleAsync([AsParameters] SearchCoursesRequest request, ICourseService service, CancellationToken ct)
    {
        SearchCoursesInput input = new()
        {
            Title = request.Title,
            SubcategoryId = request.SubcategoryId
        };

        ApplicationResult<IReadOnlyList<CourseOutput>> result = await service.SearchCoursesAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Ok(result.Data);
    }
}
