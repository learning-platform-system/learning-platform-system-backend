using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers;
using LearningPlatformSystem.Application.Teachers.Inputs;

namespace LearningPlatformSystem.API.Teachers.AddAddress;

public static class AddTeacherAddressEndpoint
{
    public static RouteGroupBuilder MapPostTeacherAddressEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{TeacherId:guid}/addresses", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid teacherId, AddTeacherAddressRequest request, ITeacherService service, CancellationToken ct)
    {
        AddTeacherAddressInput input = new(teacherId, request.Street, request.PostalCode, request.City);

        ApplicationResult result = await service.AddTeacherAddressAsync(input, ct);

        if(result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Created();
    }
}
