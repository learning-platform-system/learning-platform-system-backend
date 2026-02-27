using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Teachers;

public static class TeacherApplicationErrors
{
    public static ApplicationResultError EmailAlreadyExists(string email)
    {
        return new ApplicationResultError(ErrorTypes.Conflict, $"En lärare med mejladressen {email} finns redan");
    }

    public static ApplicationResultError NotFound(Guid id)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Ingen lärare med id {id} hittades");
    }
}
