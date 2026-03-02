using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.CoursePeriods;

public static class CoursePeriodApplicationErrors
{
    public static ApplicationResultError CourseNotFound(Guid courseId)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Kurs med id: {courseId} kunde inte hittas.");
    }

    public static ApplicationResultError CampusNotFound(Guid? campusId)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Campus med id: {campusId} kunde inte hittas.");
    }

    public static ApplicationResultError NotFound(Guid coursePeriodId)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Kursperiod med id: {coursePeriodId} kunde inte hittas.");
    }

    public static ApplicationResultError TeacherNotFound(Guid teacherId)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Lärare med id: {teacherId} kunde inte hittas.");
    }

    public static ApplicationResultError CampusIdIsRequired(Guid? campusId)
    {
        return new ApplicationResultError(ErrorTypes.BadRequest, $"CampusId är obligatoriskt. Värdet som angavs var: {campusId}");
    }
}
