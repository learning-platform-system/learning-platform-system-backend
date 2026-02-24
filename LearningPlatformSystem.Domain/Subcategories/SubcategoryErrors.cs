namespace LearningPlatformSystem.Domain.Subcategories;

public static class SubcategoryErrors
{
    public const string NameIsRequired = "Subkategorins namn måste anges.";
    public static string SubcategoryNameIsTooLong(int nameMaxLength) => $"Subkategorins namn får inte vara längre än {nameMaxLength} tecken.";


    public const string CategoryIdIsRequired = "Kategori-id måste anges.";
}
