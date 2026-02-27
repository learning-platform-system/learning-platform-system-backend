using LearningPlatformSystem.API.Campuses.Create;

namespace LearningPlatformSystem.API.Campuses;

public static class CampusEndpoints
{
    public static IEndpointRouteBuilder MapCampusEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/campuses")
            .WithTags("Campuses");

        group.MapPostCampusEndpoint();
        //group.MapDeleteCampusEndpoint();
        //group.MapGetAllCampusesEndpoint();

        return app;
    }   
}
