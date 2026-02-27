using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Campuses;

public static class CampusApplicationErrors
{
    public static ApplicationResultError NameAlreadyExists(string name)
    {
        return new ApplicationResultError(ErrorTypes.Conflict, $"Ett campus med namnet {name} finns redan.");
    }
}
