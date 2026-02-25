using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.CourseSessionAttendances;

namespace LearningPlatformSystem.API.CoursePeriods.AddCourseSessionAttendace;

public static class AddCourseSessionAttendanceEndpoint
{
    public static RouteGroupBuilder MapPostCourseSessionAttendanceEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{coursePeriodId:guid}/courseSessions/{courseSessionId:guid}/attendances", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid coursePeriodId, Guid courseSessionId, AddCourseSessionAttendanceRequest request, ICoursePeriodService service, CancellationToken ct)
    {
        if (!Enum.TryParse(request.Status, true, out AttendanceStatus parsedStatus))
        {
            return Results.BadRequest("Ogiltig närvarostatus.");
        }

        AddCourseSessionAttendanceInput input = new(request.StudentId, courseSessionId, coursePeriodId, parsedStatus);

        ApplicationResult result = await service.AddAttendanceAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Created();
    }
}
