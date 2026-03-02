using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Categories.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Categories.AddSubcategory;

public static class AddSubcategoryEndpoint
{
    public static RouteGroupBuilder MapPostSubcategoryEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("{categoryId:guid}/subcategories", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(Guid categoryId, AddSubcategoryRequest request, ICategoryService service, CancellationToken ct)
    {
        AddSubcategoryInput input = new(categoryId, request.Name);

        ApplicationResult result = await service.AddSubcategoryAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.Created();
    }
}

