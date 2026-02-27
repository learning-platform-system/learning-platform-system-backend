
using LearningPlatformSystem.API.Teachers.AddAddress;
using LearningPlatformSystem.API.Teachers.Create;
using LearningPlatformSystem.API.Teachers.Delete;
using LearningPlatformSystem.API.Teachers.GetAll;

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
        group.MapGetAllTeachersEndpoint();



        return app;
    }
}
