using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;
using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;

namespace LearningPlatformSystem.Domain.Campuses;

public class Campus
{
    public const int CampusNameMaxLength = 50;

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public ContactInformation? ContactInformation { get; private set; }
    public Address Address { get; private set; } 

    private Campus(Guid id, string name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    public static Campus Create(string name, string street, string postalCode, string city)
    {
        string normalizedName = ValidateName(name);

        Address address = Address.Create(street, postalCode, city);

        Guid id = Guid.NewGuid();
        Campus campus = new(id, normalizedName, address);

        return campus;
    }

    public void ChangeName(string name)
    {
        string normalizedName = ValidateName(name);

        Name = normalizedName;
    }

    public void ChangeAddress(string street, string postalCode, string city)
    {
        Address = Address.Create(street, postalCode, city);
    }

    public void AddContactInformation(string email, string phoneNumber)
    {
        if (ContactInformation is not null)
        {
            throw new DomainException(CampusErrors.ContactInformationAlreadyExists);
        }

        ContactInformation = ContactInformation.Create(email, phoneNumber);
    }

    public void ChangeContactInformation(string email, string phoneNumber)
    {
        if (ContactInformation is null) 
        {
            throw new DomainException(CampusErrors.ContactInformationIsRequired);
        }
        
        ContactInformation = ContactInformation.Create(email, phoneNumber);
    }

    private static string ValidateName(string name)
    {
        return DomainValidator.ValidateRequiredString(name, CampusNameMaxLength,
            CampusErrors.CampusNameIsRequired, CampusErrors.CampusNameIsTooLong(CampusNameMaxLength));
    }
}
    
