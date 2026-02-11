using LearningPlatformSystem.Domain.Addresses;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.ContactInformations;

public class ContactInformation
{
    private ContactInformation() { } // parameterlös konstruktor som krävs av EF Core

    public const int EmailMaxLength = 100;
    public const int PhoneNumberMaxLength = 20;
    public const int PhoneNumberMinLength = 8;

    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public Address? Address { get; private set; }


    private ContactInformation(Guid id, string email, string phoneNumber)
    {
        Id = id;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static ContactInformation Create(string email, string phoneNumber)
    {
        string normalizedEmail = DomainValidator.ValidateRequiredString(email, EmailMaxLength, 
            ContactInformationErrors.EmailIsRequired, ContactInformationErrors.EmailIsTooLong(EmailMaxLength));

        string normalizedPhoneNumber = DomainValidator.ValidateRequiredStringWithLengthRange(phoneNumber, PhoneNumberMinLength, PhoneNumberMaxLength,
            ContactInformationErrors.PhoneNumberIsRequired, ContactInformationErrors.PhoneNumberIsTooShort(PhoneNumberMinLength), 
            ContactInformationErrors.PhoneNumberIsTooLong(PhoneNumberMaxLength));

        Guid id = Guid.NewGuid();
        ContactInformation contactInformation = new(id, normalizedEmail, normalizedPhoneNumber);

        return contactInformation;
    }

    //Ska kunna lägga till kontaktinformation utan att ange adress, och sedan kunna lägga till adressen senare. Därför har jag gjort en metod för att lägga till adressen separat.
    public void AddAddress(string streetName, string postalCode, string city)
    {
        Address = Address.Create(streetName, postalCode, city);
    }
}
