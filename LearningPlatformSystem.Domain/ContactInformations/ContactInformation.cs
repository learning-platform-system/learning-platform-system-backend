using LearningPlatformSystem.Domain.Addresses;
using System.Xml.Linq;

namespace LearningPlatformSystem.Domain.ContactInformations;

public class ContactInformation
{
    public const int EmailMaxLength = 100;
    public const int PhoneNumberMaxLength = 20;
    public const int PhoneNumberMinLength = 8;

    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Address? Address { get; set; }


    private ContactInformation(Guid id, string email, string phoneNumber)
    {
        Id = id;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static ContactInformation Create(string email, string phoneNumber)
    {
        string normalizedEmail = email?.Trim() ?? string.Empty;
        string normalizedPhoneNumber = phoneNumber?.Trim() ?? string.Empty;

        ValidateEmail(normalizedEmail);
        ValidatePhoneNumber(normalizedPhoneNumber);

        Guid id = Guid.NewGuid();
        ContactInformation contactInformation = new(id, normalizedEmail, normalizedPhoneNumber);

        return contactInformation;
    }

    //Ska kunna lägga till kontaktinformation utan att ange adress, och sedan kunna lägga till adressen senare. Därför har jag gjort en metod för att lägga till adressen separat.
    public void AddAddress(string streetName, string postalCode, string city)
    {
        Address = Address.Create(streetName, postalCode, city);
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new EmailIsRequired();
        }
        if (email.Length > EmailMaxLength)
        {
            throw new EmailIsTooLong(EmailMaxLength);
        }
    }

    public static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new PhoneNumberIsRequired();
        }
        if (phoneNumber.Length > PhoneNumberMaxLength)
        {
            throw new PhoneNumberIsTooLong(PhoneNumberMaxLength);
        }
        if (phoneNumber.Length < PhoneNumberMinLength)
        {
            throw new PhoneNumberIsTooShort(PhoneNumberMinLength);
        }
    }
}
