using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Domain.Categories;

public class Category
{
    // private - listan är bara tillgänglig inne i category. Readonly, listan är muterbar, men referensen kan inte bytas ut (alltid samma lista-instans)
    private readonly List<Subcategory> _subcategories = new();
    // följa databasens satta name length - säkerhet. Bara props blir kolumner i databasen
    public const int NameMaxLength = 100;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    // för att kunna få tag på listan i application och läsa den 
    public IReadOnlyCollection<Subcategory> Subcategories => _subcategories;


    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }


    public void AddSubcategory(string name)
    {
        var subcategory = Subcategory.Create(Id, name);
        _subcategories.Add(subcategory);
    }


    // skapa upp regler för vad en Category får vara
    public static Category Create(string name)
    {
        string normalizedName = name?.Trim() ?? string.Empty;

        // application fångar upp i catch, man ser tydligt vad det är för exception
        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new CategoryNameIsRequired();
        }

        if (normalizedName.Length > NameMaxLength) 
        {
            throw new CategoryNameTooLongException(NameMaxLength);
        }

        Guid id = Guid.NewGuid();
        Category category = new(id, normalizedName);

        return category;
    }
}
