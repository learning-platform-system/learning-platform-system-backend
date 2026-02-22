using LearningPlatformSystem.API.Classrooms.Create;
using LearningPlatformSystem.API.Classrooms.Delete;

namespace LearningPlatformSystem.API.Classrooms;

public static class ClassroomEndpoints
{
    public static IEndpointRouteBuilder MapClassroomEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/classrooms")
            .WithTags("Classrooms");

        group.MapCreateClassroomEndpoint();
        group.MapDeleteClassroomEndpoint();


        return app;
    }
}
