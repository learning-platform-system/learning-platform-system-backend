using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students;
using LearningPlatformSystem.Application.Students.Inputs;

namespace LearningPlatformSystem.API.Students.AddAddress;

public static class AddStudentAddressEndPoint
{
    public static RouteGroupBuilder MapPostStudentAddressEndPoint(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/address", HandleAsync);
        return group;
    }
    private static async Task<IResult> HandleAsync(Guid id, AddStudentAddressRequest request, IStudentService service, CancellationToken ct)
    {
        AddStudentAddressInput input = new(id, request.Street, request.PostalCode, request.City);

        ApplicationResult result = await service.AddStudentAddressAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.NoContent();
    }
}
