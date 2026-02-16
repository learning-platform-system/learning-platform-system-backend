using LearningPlatformSystem.Domain.Shared.Validators;
using LearningPlatformSystem.Domain.Shared.ValueObjects;

namespace LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;

public class ContactInformation : ValueObject
{
    public const int EmailMaxLength = 100;
    public const int PhoneNumberMaxLength = 20;
    public const int PhoneNumberMinLength = 8;

    public string Email { get; } 
    public string PhoneNumber { get; }

    private ContactInformation() { } // parameterlös konstruktor som krävs av EF Core
    private ContactInformation(string email, string phoneNumber)
    {
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

        ContactInformation contactInformation = new(normalizedEmail, normalizedPhoneNumber);

        return contactInformation;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return PhoneNumber;
    }
}
