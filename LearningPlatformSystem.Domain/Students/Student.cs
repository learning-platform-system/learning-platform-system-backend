using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

namespace LearningPlatformSystem.Domain.Students;

public class Student
{
    public Guid Id { get; private set; }
    public PersonName Name { get; private set; } 
    public ContactInformation ContactInformation { get; private set; }
    public Address? Address { get; private set; }

    private Student(Guid id, PersonName name, ContactInformation contactInformation)
    {
        Id = id;
        Name = name;
        ContactInformation = contactInformation;
    }

    public static Student Create(string firstName, string lastName, string email, string phoneNumber)
    {
        PersonName name = PersonName.Create(firstName, lastName);

        ContactInformation contactInformation = ContactInformation.Create(email, phoneNumber);

        Guid id = Guid.NewGuid();
        Student student = new(id, name, contactInformation);

        return student;
    }

    public void ChangeName(string firstName, string lastName)
    {
        Name = PersonName.Create(firstName, lastName);
    }

    public void AddAddress(string street, string postalCode, string city)
    {
        if (Address is not null)
        {
            throw new DomainException(StudentErrors.AddressAlreadyExists);
        }
        
        Address = Address.Create(street, postalCode, city);
    }

    public void ChangeAddress(string street, string postalCode, string city)
    {
        if (Address is null)
        {
            throw new DomainException(StudentErrors.AddressIsRequired);
        }

        Address = Address.Create(street, postalCode, city);

    }

    public void ChangeContactInformation(string email, string phoneNumber)
    {
        ContactInformation = ContactInformation.Create(email, phoneNumber);
    }
}
