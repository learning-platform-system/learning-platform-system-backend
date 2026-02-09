using LearningPlatformSystem.Domain.Addresses;

namespace LearningPlatformSystem.Domain.Campuses;

public class Campus
{
    public const int NameMaxLength = 50;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Address Address { get; set; }

    private Campus(Guid id, string name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    public static Campus Create(string name, string streetName, string postalCode, string city)
    {
        string normalizedName = name?.Trim() ?? string.Empty;

        ValidateName(normalizedName);

        Address address = Address.Create(streetName, postalCode, city);

        Guid id = Guid.NewGuid();
        Campus campus = new(id, normalizedName, address);

        return campus;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CampusNameIsRequired();
        }
        if (name.Length > NameMaxLength)
        {
            throw new CampusNameIsTooLong(NameMaxLength);
        }
    }
}
    
