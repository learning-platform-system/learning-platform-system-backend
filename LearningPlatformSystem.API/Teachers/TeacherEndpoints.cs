
using LearningPlatformSystem.API.Teachers.AddAddress;
using LearningPlatformSystem.API.Teachers.Create;
using LearningPlatformSystem.API.Teachers.Delete;

namespace LearningPlatformSystem.API.Teachers;

public static class TeacherEndpoints
{
    public static IEndpointRouteBuilder MapTeacherEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/teachers")
            .WithTags("Teachers");

        group.MapPostTeacherEndpoint();
        group.MapPostTeacherAddressEndpoint();
        group.MapDeleteTeacherEndpoint();



        return app;
    }
}
