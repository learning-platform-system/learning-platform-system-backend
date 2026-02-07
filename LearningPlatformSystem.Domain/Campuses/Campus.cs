using LearningPlatformSystem.Domain.Addresses;

namespace LearningPlatformSystem.Domain.Campuses;

public class Campus
{
    public const int NameMaxLength = 50;

    public Guid Id { get; set; }
    public Guid AddressId { get; set; }
    public string Name { get; set; } = null!;

    private Campus(Guid id, Guid addressId, string name)
    {
        Id = id;
        AddressId = addressId;
        Name = name;
    }

    public static Campus Create(string name, Guid addressId)
    {
        ValidateName(name);

        Guid id = Guid.NewGuid();
        Campus campus = new(id, addressId, name);

        return campus;
    }

    public static void ValidateName(string name)
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
    
