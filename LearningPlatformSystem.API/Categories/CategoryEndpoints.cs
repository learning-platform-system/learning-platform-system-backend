using LearningPlatformSystem.API.Categories.Create;
using LearningPlatformSystem.API.Categories.Delete;
using LearningPlatformSystem.API.Categories.GetById;
using LearningPlatformSystem.API.Categories.Update;

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
        group.MapPutCategoryNameEndpoint();

        return app;
    }
}
