using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Campuses;

public static class CampusApplicationErrors
{
    public static ApplicationResultError NameAlreadyExists(string name)
    {
        return new ApplicationResultError(ErrorTypes.Conflict, $"Ett campus med namnet {name} finns redan.");
    }

    public static ApplicationResultError NotFound(Guid id)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Ett campus med id {id} kunde inte hittas.");
    }
}
