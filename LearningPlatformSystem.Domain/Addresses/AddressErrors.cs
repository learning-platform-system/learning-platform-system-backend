namespace LearningPlatformSystem.Domain.Addresses;

public static class AddressErrors
{
    public const string StreetNameIsRequired = "Gatunamn måste anges.";
    //metod för att få tag på längd-const i Address
    public static string StreetNameIsTooLong(int streetNameMaxLength) => 
        $"Gatunamn får inte vara längre än {streetNameMaxLength} tecken.";

    public const string PostalCodeIsRequired = "Postnummer måste anges.";
    public static string PostalCodeIsTooLong(int postalCodeMaxLength) =>
        $"Postnummret får inte vara längre än {postalCodeMaxLength} tecken.";

    public const string CityIsRequired = "Stad måste anges.";
    public static string CityIsTooLong(int cityMaxLength) => 
        $"Stad får inte vara längre än {cityMaxLength} tecken.";


}
