using LearningPlatformSystem.Domain.Categories;

namespace LearningPlatformSystem.Domain.Subcategories;

public class Subcategory
{
    public const int NameMaxLength = 100;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;

    // eventuellt en lista med Courses

    private Subcategory(Guid id, string name)
    {
        Id = id;
        Name = name;
    }


    public static Subcategory Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new SubcategoryNameIsRequired();
        }

        if (name.Length > Category.NameMaxLength)
        {
            throw new SubcategoryNameIsTooLong(NameMaxLength);
        }

        Guid id = Guid.NewGuid();
        Subcategory subcategory = new(id,name);

        return subcategory;
    }



}
