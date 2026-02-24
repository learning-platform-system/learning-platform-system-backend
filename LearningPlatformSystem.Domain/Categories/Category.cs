using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;
using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Domain.Categories;

public sealed class Category
{
    // private - listan är bara tillgänglig inne i category. Readonly, listan är muterbar, men referensen kan inte bytas ut (alltid samma lista-instans)
    private readonly List<Subcategory> _subcategories = new();

    // följa databasens satta namelength - säkerhet. Bara props blir kolumner i databasen
    public const int NameMaxLength = 100;

    public Guid Id { get; private set; }
    public string Name { get; private set; } 

    // för att kunna få tag på listan i application och läsa den 
    public IReadOnlyCollection<Subcategory> Subcategories => _subcategories;


    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create(Guid id, string name)
    {
        string normalizedName = ValidateName(name);

        Category category = new(id, normalizedName);

        return category;
    }

    public void ChangeName(string name)
    {
        string normalizedName = ValidateName(name);

        if (Name == normalizedName)
            return;

        Name = normalizedName;
    }

    // för att infrastructure ska kunna återskapa domänobjektet från databasen 
    internal static Category BuildFromDatabase(Guid id, string name, IEnumerable<Subcategory> subCategories)
    {
        Category category = new Category(id, name);

        category._subcategories.AddRange(subCategories);

        return category;
    }

    public void AddSubcategory(string name)
    {
        var subcategory = Subcategory.Create(Guid.NewGuid(), Id, name);
        _subcategories.Add(subcategory);
    }

    public void EnsureCanBeRemoved()
    {
        if (_subcategories.Any())
            throw new DomainException(CategoryErrors.CannotBeRemoved);
    }

    private static string ValidateName(string name)
    {
        string normalizedName = DomainValidator.ValidateRequiredString(name, NameMaxLength,
            CategoryErrors.NameIsRequired, CategoryErrors.NameIsTooLong(NameMaxLength));

        return normalizedName;
    }
}
