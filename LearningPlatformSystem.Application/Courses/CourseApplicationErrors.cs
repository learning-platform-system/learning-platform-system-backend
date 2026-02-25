using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Courses;

public static class CourseApplicationErrors
{
    public static ApplicationResultError TitleAlreadyExists(string title)
    {
        return new ApplicationResultError(ErrorTypes.Conflict, $"En kurs med titeln {title} finns redan.");
    }

    public static ApplicationResultError SubcategoryNotFound(Guid id)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Subkategorin med id {id} hittades inte.");
    }
}
