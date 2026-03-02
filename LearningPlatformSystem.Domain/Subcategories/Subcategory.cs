using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.Subcategories;

public sealed class Subcategory
{
    public const int NameMaxLength = 100;

    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; } 

    private Subcategory(Guid id, Guid categoryId, string name)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
    }

    // internal, måste skapas via category (application kommer inte åt), en subcategory måste tillhöra en category
    internal static Subcategory Create(Guid id, Guid categoryId, string name)
    {
        DomainValidator.ValidateRequiredGuid(categoryId, SubcategoryErrors.CategoryIdIsRequired);

        string normalizedName = DomainValidator.ValidateRequiredString(name, NameMaxLength, 
            SubcategoryErrors.NameIsRequired, SubcategoryErrors.SubcategoryNameIsTooLong(NameMaxLength));

        Subcategory subcategory = new(id, categoryId, normalizedName);

        return subcategory;
    }

    // Används endast av CategoryRepository för att bygga upp objekt från databasen
    internal static Subcategory Rehydrate(Guid id, Guid categoryId, string name)
    {
        return new Subcategory(id, categoryId, name);
    }
}
