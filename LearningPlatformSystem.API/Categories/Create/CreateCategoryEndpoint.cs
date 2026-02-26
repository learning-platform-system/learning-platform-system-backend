using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Categories.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Categories.Create;

public static class CreateCategoryEndpoint
{
    public static RouteGroupBuilder MapPostCategoryEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(CreateCategoryRequest request, ICategoryService service, CancellationToken ct)
    {
        CreateCategoryInput input = new(request.Name);

        ApplicationResult<Guid> result = await service.CreateCategoryAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.Created($"/categories/{result.Data}", new { id = result.Data });
    }
}
