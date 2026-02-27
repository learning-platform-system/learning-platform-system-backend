using LearningPlatformSystem.API.Shared;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformSystem.API.CoursePeriods.AddCourseSession;

public static class AddCourseSessionEndpoint
{
    public static RouteGroupBuilder MapPostCourseSessionEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{coursePeriodId:guid}/sessions", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid coursePeriodId, AddCourseSessionRequest request, ICoursePeriodService service, CancellationToken ct)
    {
        if (!Enum.TryParse(request.Format, true, out CourseFormat parsedFormat))
        {
            return Results.BadRequest("Ogiltigt kursformat.");
        }
        AddCourseSessionInput input = new(
            coursePeriodId,
            parsedFormat,
            request.ClassroomId,
            request.Date,
            request.StartTime,
            request.EndTime
        );

        ApplicationResult result = await service.AddSessionAsync(input, ct);

        if (result.IsFailure)
        {
            return result.ToHttpFailResult();
        }
        return Results.Created();
    }
}
