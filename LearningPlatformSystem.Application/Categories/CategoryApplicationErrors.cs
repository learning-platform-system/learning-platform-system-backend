using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Categories;

public static class CategoryApplicationErrors
{
    public static ApplicationResultError NameAlreadyExists(string name)
    {
        return new ApplicationResultError(ErrorTypes.Conflict, $"En kategori med namnet {name} finns redan.");
    }

    public static ApplicationResultError BadRequest(string message)
    {
        return new ApplicationResultError (ErrorTypes.BadRequest, message);
    }
}
