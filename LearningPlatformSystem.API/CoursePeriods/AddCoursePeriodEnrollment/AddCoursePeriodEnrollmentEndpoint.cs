using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.CoursePeriods.AddCoursePeriodEnrollment;

public static class AddCoursePeriodEnrollmentEndpoint
{
    public static RouteGroupBuilder MapPostCoursePeriodEnrollmentEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{CoursePeriodId:guid}/enrollments", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid coursePeriodId, AddCoursePeriodEnrollmentRequest request, ICoursePeriodService service, CancellationToken ct)
    {
        AddCoursePeriodEnrollmentInput input = new(request.StudentId, coursePeriodId);

        ApplicationResult result = await service.AddEnrollmentAsync(input, ct);

        if(result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Created();
    }
}
