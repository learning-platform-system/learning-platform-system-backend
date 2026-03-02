using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Campuses;
using LearningPlatformSystem.Application.Campuses.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Campuses.AddContactInformation;

public static class AddCampusContactInformationEndpoint
{
    public static RouteGroupBuilder MapPostCampusContactInformationEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/contact-information", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, AddCampusContactInformationRequest request, ICampusService service, CancellationToken ct)
    {
        AddCampusContactInformationInput input = new(id, request.Email, request.PhoneNumber);

        ApplicationResult result = await service.AddCampusContactInformationAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();   
        }

        return Results.NoContent();
    }
}