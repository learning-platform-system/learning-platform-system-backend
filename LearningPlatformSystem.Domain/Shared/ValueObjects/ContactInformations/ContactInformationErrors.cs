namespace LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;

public static class ContactInformationErrors
{
    public const string EmailIsRequired = "Email måste anges.";
    public static string EmailIsTooLong(int emailMaxLength) => $"Email får inte vara längre än {emailMaxLength} tecken.";


    public const string PhoneNumberIsRequired =
    "Telefonnummer måste anges.";

    public const string AddressAlreadyExists = "Adress finns redan.";

    public const string AddressIsRequired = "Ingen address finns att ändra.";

    public static string PhoneNumberIsTooShort(int minLength) =>
        $"Telefonnummret måste vara minst {minLength} tecken.";
    public static string PhoneNumberIsTooLong(int maxLength) =>
        $"Telefonnummret får inte vara längre än {maxLength} tecken.";
}
