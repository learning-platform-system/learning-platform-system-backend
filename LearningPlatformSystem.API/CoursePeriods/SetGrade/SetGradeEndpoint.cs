using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.CoursePeriodEnrollments;

namespace LearningPlatformSystem.API.CoursePeriods.SetGrade;

public static class SetGradeEndpoint
{
    public static RouteGroupBuilder MapPutGradeEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("{coursePeriodId:guid}/grade", HandleAsync);
        return group;
    }
    private static async Task<IResult> HandleAsync(Guid coursePeriodId, SetGradeRequest request, ICoursePeriodService service, CancellationToken ct)
    {
        if (!Enum.TryParse(request.Grade, true, out Grade parsedGrade))
        {
            return Results.BadRequest("Ogiltigt betyg.");
        }

        SetGradeInput input = new
            (
            CoursePeriodId: coursePeriodId,
            StudentId: request.StudentId,
            Grade: parsedGrade
            );

        ApplicationResult result = await service.SetCoursePeriodGradeAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.NoContent();
    }
}
