using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Categories.Delete;

public static class DeleteCategoryEndpoint
{
    public static RouteGroupBuilder MapDeleteCategoryEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}", HandleAsync);

        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, ICategoryService service, CancellationToken ct)
    {

        ApplicationResult result = await service.DeleteCategoryAsync(id, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }

        return Results.NoContent();
    }
}
