using LearningPlatformSystem.API.CoursePeriods.AddCourseSession;
using LearningPlatformSystem.API.CoursePeriods.Create;

namespace LearningPlatformSystem.API.CoursePeriods;

public static class CoursePeriodEndpoints
{
    public static IEndpointRouteBuilder MapCoursePeriodEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/coursePeriods")
            .WithTags("CoursePeriods");

        group.MapPostCoursePeriodEndpoint();
        group.MapPostCourseSessionEndpoint();


        return app;
    }
}