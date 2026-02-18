using LearningPlatformSystem.Domain.Shared.Validators;
using LearningPlatformSystem.Domain.Shared.ValueObjects;

namespace LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;

public class Address : ValueObject
{
    public const int StreetMaxLength = 50;
    public const int CityMaxLength = 50;
    public const int PostalCodeMaxLength = 6;

    public string Street { get; } 
    public string PostalCode { get; }
    public string City { get; } 

    private Address(string street, string postalCode, string city)
    {
        Street = street;
        PostalCode = postalCode;
        City = city;
    }

    // måste skapas via contact information (application kommer inte åt), en adress måste tillhöra antingen en contactInformation eller en campus
    internal static Address Create(string streetName, string postalCode, string city)
    {
        string normalizedStreetName = DomainValidator.ValidateRequiredString(streetName, StreetMaxLength,
            AddressErrors.StreetIsRequired, AddressErrors.StreetIsTooLong(StreetMaxLength));

        string normalizedPostalCode = DomainValidator.ValidateRequiredString(postalCode, PostalCodeMaxLength,
            AddressErrors.PostalCodeIsRequired, AddressErrors.PostalCodeIsTooLong(PostalCodeMaxLength));

        string normalizedCity = DomainValidator.ValidateRequiredString(city, CityMaxLength,
            AddressErrors.CityIsRequired, AddressErrors.CityIsTooLong(CityMaxLength));

        Address address = new(normalizedStreetName, normalizedPostalCode, normalizedCity);

        return address;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return PostalCode;
        yield return City;
    }
}

