using LearningPlatformSystem.API.CoursePeriods.AddCoursePeriodEnrollment;
using LearningPlatformSystem.API.CoursePeriods.AddCoursePeriodResource;
using LearningPlatformSystem.API.CoursePeriods.AddCoursePeriodReview;
using LearningPlatformSystem.API.CoursePeriods.AddCourseSession;
using LearningPlatformSystem.API.CoursePeriods.AddCourseSessionAttendace;
using LearningPlatformSystem.API.CoursePeriods.Create;
using LearningPlatformSystem.API.CoursePeriods.Delete;
using LearningPlatformSystem.API.CoursePeriods.GetByCourseId;
using LearningPlatformSystem.API.CoursePeriods.SetGrade;

namespace LearningPlatformSystem.API.CoursePeriods;

public static class CoursePeriodEndpoints
{
    public static IEndpointRouteBuilder MapCoursePeriodEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/coursePeriods")
            .WithTags("CoursePeriods");

        group.MapPostCoursePeriodEndpoint();
        group.MapPostCourseSessionEndpoint();
        group.MapPostCoursePeriodResourceEndpoint();
        group.MapPostCoursePeriodReviewEndpoint();
        group.MapPostCoursePeriodEnrollmentEndpoint();
        group.MapPutGradeEndpoint();
        group.MapPostCourseSessionAttendanceEndpoint();
        group.MapGetCoursePeriodsByCourseIdEndpoint();
        group.MapDeleteCoursePeriodEndpoint();


        return app;
    }
}