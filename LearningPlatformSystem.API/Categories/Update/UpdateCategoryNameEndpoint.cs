using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Categories.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Categories.Update;

public static class UpdateCategoryNameEndpoint
{
    public static RouteGroupBuilder MapPutCategoryNameEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("{id:guid}", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, UpdateCategoryNameRequest request, ICategoryService service, CancellationToken ct)
    {
        UpdateCategoryNameInput input = new
            (
            Id: request.Id,
            Name: request.Name
            );

        ApplicationResult result = await service.UpdateNameAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.NoContent();
    }
}
