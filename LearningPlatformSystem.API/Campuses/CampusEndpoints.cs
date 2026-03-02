using LearningPlatformSystem.API.Campuses.AddContactInformation;
using LearningPlatformSystem.API.Campuses.Create;
using LearningPlatformSystem.API.Campuses.Delete;
using LearningPlatformSystem.API.Campuses.GetAll;

namespace LearningPlatformSystem.API.Campuses;

public static class CampusEndpoints
{
    public static IEndpointRouteBuilder MapCampusEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/campuses")
            .WithTags("Campuses");

        group.MapPostCampusEndpoint();
        group.MapDeleteCampusEndpoint();
        group.MapGetAllCampusesEndpoint();
        group.MapPostCampusContactInformationEndpoint();

        return app;
    }   
}
