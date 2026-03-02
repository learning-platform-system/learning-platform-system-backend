using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Categories;

public static class CategoryApplicationErrors
{
    public static ApplicationResultError NameAlreadyExists(string name)
    {
        return new ApplicationResultError(ErrorTypes.Conflict, $"En kategori med namnet {name} finns redan.");
    }

    public static ApplicationResultError NotFound(Guid id)
    {
        return new ApplicationResultError(ErrorTypes.NotFound, $"Kategorin med id: {id} kunde inte hittas.");
    }
}
