using LearningPlatformSystem.API.Courses.Create;
using LearningPlatformSystem.API.Courses.Delete;
using LearningPlatformSystem.API.Courses.GetById;

namespace LearningPlatformSystem.API.Courses;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCourseEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/courses")
            .WithTags("Courses");

        group.MapPostCourseEndpoint();
        group.MapGetCourseByIdEndpoint();
        group.MapDeleteCourseEndpoint();

        return app;
    }
}
