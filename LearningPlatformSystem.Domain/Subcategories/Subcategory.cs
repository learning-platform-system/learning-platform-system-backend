using LearningPlatformSystem.Domain.Categories;

namespace LearningPlatformSystem.Domain.Subcategories;

public class Subcategory
{
    public const int NameMaxLength = 100;

    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; } = null!;

    // eventuellt en lista med Courses

    private Subcategory(Guid id, Guid categoryId, string name)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
    }

    // måste skapas via category (application kommer inte åt), en subcategory måste tillhöra en category
    internal static Subcategory Create(Guid categoryId, string name)
    {
        string normalizedName = name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new SubcategoryNameIsRequired();
        }

        if (normalizedName.Length > NameMaxLength)
        {
            throw new SubcategoryNameIsTooLong(NameMaxLength);
        }

        Guid id = Guid.NewGuid();
        Subcategory subcategory = new(id, categoryId, normalizedName);

        return subcategory;
    }



}
