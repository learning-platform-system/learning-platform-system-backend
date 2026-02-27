using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Students;

public static class StudentApplicationErrors
{
    public static ApplicationResultError EmailAlreadyExists(string email)
    {
        return new ApplicationResultError(ErrorTypes.Conflict, $"En student med email {email} finns redan.");
    }

    public static ApplicationResultError NotFound(Guid id)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Ingen student med id {id} hittades.");
    }
}
