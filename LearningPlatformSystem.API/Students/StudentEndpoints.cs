using LearningPlatformSystem.API.Students.Create;

namespace LearningPlatformSystem.API.Students;

public static class StudentEndpoints
{
    public static IEndpointRouteBuilder MapStudentEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/students")
            .WithTags("Students");

        group.MapPostStudentEndpoint();

        return app;
    }

}
