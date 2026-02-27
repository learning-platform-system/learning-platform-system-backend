using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Campuses;
using LearningPlatformSystem.Application.Campuses.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Campuses.Create;

public static class CreateCampusEndpoint
{
    public static RouteGroupBuilder MapPostCampusEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(CreateCampusRequest request, ICampusService service, CancellationToken ct)
    {
        CreateCampusInput input= new(request.Name, request.Street, request.PostalCode, request.City);

        ApplicationResult<Guid> result = await service.CreateCampusAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        
        return Results.Created($"/campuses/{result.Data}", new { id = result.Data });
    }
}
   
