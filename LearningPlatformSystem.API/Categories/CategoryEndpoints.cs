using LearningPlatformSystem.API.Categories.Create;
using LearningPlatformSystem.API.Categories.Delete;
using LearningPlatformSystem.API.Categories.GetById;

namespace LearningPlatformSystem.API.Categories;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/categories")
            .WithTags("Categories");

        group.MapPostCategoryEndpoint();
        group.MapDeleteCategoryEndpoint();
        group.MapGetCategoryEndpoint();


        return app;
    }
}
