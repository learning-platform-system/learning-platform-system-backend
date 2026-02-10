namespace LearningPlatformSystem.Domain.Categories;

public static class CategoryErrors
{
    public const string CategoryNameIsRequired = "Kategorinamn måste anges.";
    public static string CategoryNameIsTooLong(int categoryNameMaxLength) =>
        $"Kategorinamnet får inte vara längre än {categoryNameMaxLength} tecken.";
}
