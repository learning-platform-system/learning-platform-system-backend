namespace LearningPlatformSystem.Domain.Subcategories;

public static class SubcategoryErrors
{
    public const string SubcategoryNameIsRequired = "Subkategorins namn måste anges.";
    public static string SubcategoryNameIsTooLong(int subcategoryNameMaxLength) => $"Subkategorins namn får inte vara längre än {subcategoryNameMaxLength} tecken.";


    public const string CategoryIdIsRequired = "Kategori-id måste anges.";
}
