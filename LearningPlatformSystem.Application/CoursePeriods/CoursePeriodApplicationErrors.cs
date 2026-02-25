using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.CoursePeriods;

public static class CoursePeriodApplicationErrors
{
    public static ApplicationResultError CourseNotFound(Guid courseId)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Kurs med id: {courseId} kunde inte hittas.");
    }

    internal static ApplicationResultError CampusNotFound(Guid? campusId)
    {
        throw new NotImplementedException();
    }

    internal static ApplicationResultError TeacherNotFound(Guid teacherId)
    {
        throw new NotImplementedException();
    }
}
