using LearningPlatformSystem.API.CoursePeriods.Create;
using LearningPlatformSystem.API.Courses.Create;

namespace LearningPlatformSystem.API.Courses;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCourseEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/courses")
            .WithTags("Courses");

        group.MapPostCourseEndpoint();


        return app;
    }
}
