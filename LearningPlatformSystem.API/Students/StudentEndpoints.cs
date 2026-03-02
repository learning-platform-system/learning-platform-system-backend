using LearningPlatformSystem.API.Students.AddAddress;
using LearningPlatformSystem.API.Students.Create;
using LearningPlatformSystem.API.Students.Delete;
using LearningPlatformSystem.API.Students.GetAll;

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
        group.MapPostStudentAddressEndPoint();
        group.MapGetAllStudentsEndpoint();

        return app;
    }

}
