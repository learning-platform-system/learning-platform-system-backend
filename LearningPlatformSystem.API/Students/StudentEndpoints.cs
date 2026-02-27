using LearningPlatformSystem.API.Students.Create;
using LearningPlatformSystem.API.Students.Delete;

namespace LearningPlatformSystem.API.Students;

public static class StudentEndpoints
{
    public static IEndpointRouteBuilder MapStudentEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/students")
            .WithTags("Students");

        group.MapPostStudentEndpoint();
        group.MapDeleteStudentEndpoint();

        return app;
    }

}
