namespace LearningPlatformSystem.Domain.Categories;

public class Category
{
    // följa databasens satta name length - säkerhet. Bara props blir kolumner i databasen
    public const int NameMaxLength = 50;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;

    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    // för att EF ska kunna skapa Category-objekt
    private Category() { }


    // skapa upp regler för vad en Category får vara
    public static Category Create(string name)
    {
        // application fångar upp i catch, man ser tydligt vad det är för exception
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CategoryNameIsRequired();
        }

        if (name.Length > NameMaxLength) 
        {
            throw new CategoryNameTooLongException(NameMaxLength);
        }

        Guid id = Guid.NewGuid();
        Category category = new(id, name);

        return category;
    }
}
