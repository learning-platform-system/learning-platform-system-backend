using LearningPlatformSystem.Domain.Shared;
using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Domain.Categories;

public class Category
{
    private Category() { } // parameterlös konstruktor som krävs av EF Core

    // private - listan är bara tillgänglig inne i category. Readonly, listan är muterbar, men referensen kan inte bytas ut (alltid samma lista-instans)
    private readonly List<Subcategory> _subcategories = new();
    // följa databasens satta namelength - säkerhet. Bara props blir kolumner i databasen
    public const int CategoryNameMaxLength = 100;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    // för att kunna få tag på listan i application och läsa den 
    public IReadOnlyCollection<Subcategory> Subcategories => _subcategories;


    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    // skapa upp regler för vad en Category får vara
    public static Category Create(string name)
    {
        string normalizedName = DomainValidator.ValidateRequiredString(name, CategoryNameMaxLength, 
            CategoryErrors.CategoryNameIsRequired, CategoryErrors.CategoryNameIsTooLong(CategoryNameMaxLength));

        Guid id = Guid.NewGuid();
        Category category = new(id, normalizedName);

        return category;
    }

    public void AddSubcategory(string name)
    {
        var subcategory = Subcategory.Create(Id, name);
        _subcategories.Add(subcategory);
    }
}
