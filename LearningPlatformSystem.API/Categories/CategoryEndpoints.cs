using LearningPlatformSystem.API.Categories.Create;
using LearningPlatformSystem.API.Classrooms.Create;
using LearningPlatformSystem.API.Classrooms.Delete;
using LearningPlatformSystem.API.Classrooms.GetByType;
using LearningPlatformSystem.API.Classrooms.Update;

namespace LearningPlatformSystem.API.Categories;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/categories")
            .WithTags("Categories");

        group.MapPostCategoryEndpoint();


        return app;
    }
}
