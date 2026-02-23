using LearningPlatformSystem.API.Classrooms.Create;
using LearningPlatformSystem.API.Classrooms.Delete;
using LearningPlatformSystem.API.Classrooms.GetByType;
using LearningPlatformSystem.API.Classrooms.Update;

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
        group.MapGetClassroomsByTypeEndpoint();
        group.MapPutClassroomEndpoint();

        return app;
    }
}
