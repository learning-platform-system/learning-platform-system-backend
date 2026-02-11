using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.Addresses;

public class Address
{
    private Address() { } // parameterlös konstruktor som krävs av EF Core

    public const int StreetNameMaxLength = 50;
    public const int CityNameMaxLength = 50;
    public const int PostalCodeMaxLength = 6;

    public Guid Id { get; private set; }
    public string StreetName { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string City { get; private set; } = null!;

    private Address(Guid id, string streetName, string postalCode, string city)
    {
        Id = id;
        StreetName = streetName;
        PostalCode = postalCode;
        City = city;
    }

    // måste skapas via contact information eller campus (application kommer inte åt), en adress måste tillhöra antingen en contactInformation eller en campus
    internal static Address Create(string streetName, string postalCode, string city)
    {

        string normalizedStreetName = DomainValidator.ValidateRequiredString(streetName, StreetNameMaxLength, 
            AddressErrors.StreetNameIsRequired, AddressErrors.StreetNameIsTooLong(StreetNameMaxLength));

        string normalizedPostalCode = DomainValidator.ValidateRequiredString(postalCode, PostalCodeMaxLength, 
            AddressErrors.PostalCodeIsRequired, AddressErrors.PostalCodeIsTooLong(PostalCodeMaxLength));

        string normalizedCity = DomainValidator.ValidateRequiredString(city, CityNameMaxLength, 
            AddressErrors.CityIsRequired, AddressErrors.CityIsTooLong(CityNameMaxLength));
        
        Guid id = Guid.NewGuid();
        Address address = new(id, normalizedStreetName, normalizedPostalCode, normalizedCity);

        return address;
    }
}
