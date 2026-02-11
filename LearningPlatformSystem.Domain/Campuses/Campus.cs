using LearningPlatformSystem.Domain.Addresses;
using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Campuses;

public class Campus
{
    private Campus() { } // parameterlös konstruktor som krävs av EF Core

    public const int CampusNameMaxLength = 50;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    private Campus(Guid id, string name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    public static Campus Create(string name, string streetName, string postalCode, string city)
    {
        string normalizedName = DomainValidator.ValidateRequiredString(name, CampusNameMaxLength, 
            CampusErrors.CampusNameIsRequired, CampusErrors.CampusNameIsTooLong(CampusNameMaxLength)); 

        Address address = Address.Create(streetName, postalCode, city);

        Guid id = Guid.NewGuid();
        Campus campus = new(id, normalizedName, address);

        return campus;
    }
}
    
