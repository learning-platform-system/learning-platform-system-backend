using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Categories.Outputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Categories.GetById;

public static class GetCategoryByIdEndpoint
{
    public static RouteGroupBuilder MapGetCategoryEndpoint(this RouteGroupBuilder group)
    {
        // id = route parameter, Guid = constraint, kräver en Guid 
        group.MapGet("{id:guid}", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, ICategoryService service, CancellationToken ct)
    {

        ApplicationResult<CategoryOutput> result = await service.GetCategoryByIdAsync(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.Ok(result.Data);
    }
}
