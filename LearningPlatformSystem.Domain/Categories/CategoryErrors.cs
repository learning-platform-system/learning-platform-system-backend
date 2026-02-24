namespace LearningPlatformSystem.Domain.Categories;

public static class CategoryErrors
{
    public const string NameIsRequired = "Kategorinamn måste anges.";

    public const string CannotBeRemoved = "Kategorin har subkategorier och kan därför inte tas bort";

    public static string NameIsTooLong(int nameMaxLength) =>
        $"Kategorinamnet får inte vara längre än {nameMaxLength} tecken.";

}
