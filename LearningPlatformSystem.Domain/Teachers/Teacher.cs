using LearningPlatformSystem.Domain.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

namespace LearningPlatformSystem.Domain.Teachers;

public class Teacher
{
    public Guid Id { get; private set; }
    public PersonName Name { get; private set; } = null!;
    public ContactInformation ContactInformation { get; private set; } = null!; 

    private Teacher(Guid id, PersonName name, ContactInformation contactInformation)
    {
        Id = id;
        Name = name;
        ContactInformation = contactInformation;
    }

    public static Teacher Create(string firstName, string lastName, string email, string phoneNumber)
    {
        PersonName name = PersonName.Create(firstName, lastName);

        ContactInformation contactInformation = ContactInformation.Create(email, phoneNumber);

        Guid id = Guid.NewGuid();
        Teacher teacher = new(id, name, contactInformation);
        return teacher;
    }
}
