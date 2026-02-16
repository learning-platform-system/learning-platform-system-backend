using LearningPlatformSystem.Domain.Shared.Validators;
using LearningPlatformSystem.Domain.Shared.ValueObjects;

namespace LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;

public class Address : ValueObject
{
    public const int StreetNameMaxLength = 50;
    public const int CityNameMaxLength = 50;
    public const int PostalCodeMaxLength = 6;

    public string StreetName { get; } 
    public string PostalCode { get; }
    public string City { get; } 

    private Address() { } // parameterlös konstruktor som krävs av EF Core
    private Address(string streetName, string postalCode, string city)
    {
        StreetName = streetName;
        PostalCode = postalCode;
        City = city;
    }

    // måste skapas via contact information (application kommer inte åt), en adress måste tillhöra antingen en contactInformation eller en campus
    internal static Address Create(string streetName, string postalCode, string city)
    {
        string normalizedStreetName = DomainValidator.ValidateRequiredString(streetName, StreetNameMaxLength,
            AddressErrors.StreetNameIsRequired, AddressErrors.StreetNameIsTooLong(StreetNameMaxLength));

        string normalizedPostalCode = DomainValidator.ValidateRequiredString(postalCode, PostalCodeMaxLength,
            AddressErrors.PostalCodeIsRequired, AddressErrors.PostalCodeIsTooLong(PostalCodeMaxLength));

        string normalizedCity = DomainValidator.ValidateRequiredString(city, CityNameMaxLength,
            AddressErrors.CityIsRequired, AddressErrors.CityIsTooLong(CityNameMaxLength));

        Address address = new(normalizedStreetName, normalizedPostalCode, normalizedCity);

        return address;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StreetName;
        yield return PostalCode;
        yield return City;
    }
}

