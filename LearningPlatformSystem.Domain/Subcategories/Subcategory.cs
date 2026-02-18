using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.Subcategories;

public class Subcategory
{
    public const int SubcategoryNameMaxLength = 100;

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
    internal static Subcategory Create(Guid categoryId, string name)
    {
        DomainValidator.ValidateRequiredGuid(categoryId, SubcategoryErrors.CategoryIdIsRequired);

        string normalizedName = DomainValidator.ValidateRequiredString(name, SubcategoryNameMaxLength, 
            SubcategoryErrors.SubcategoryNameIsRequired, SubcategoryErrors.SubcategoryNameIsTooLong(SubcategoryNameMaxLength));

        Guid id = Guid.NewGuid();
        Subcategory subcategory = new(id, categoryId, normalizedName);

        return subcategory;
    }
}
