namespace LearningPlatformSystem.Domain.Addresses;

public class Address
{
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
        string normalizedStreetName = streetName?.Trim() ?? string.Empty;
        string normalizedPostalCode = postalCode?.Trim() ?? string.Empty;
        string normalizedCity = city?.Trim() ?? string.Empty;

        ValidateStreetName(normalizedStreetName);

        ValidatePostalCode(normalizedPostalCode);

        ValidateCity(normalizedCity);
        
        Guid id = Guid.NewGuid();
        Address address = new(id, normalizedStreetName, normalizedPostalCode, normalizedCity);

        return address;
    }


    private static void ValidateStreetName(string streetName)
    {
        if (string.IsNullOrWhiteSpace(streetName))
        {
            throw new StreetNameIsRequired();
        }

        if (streetName.Length > CityNameMaxLength)
        {
            throw new StreetNameTooLong(CityNameMaxLength);
        }
    }

    private static void ValidatePostalCode(string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new PostalCodeIsRequired();
        }
        if (postalCode.Length > PostalCodeMaxLength)
        {
            throw new PostalCodeTooLong(PostalCodeMaxLength);
        }
    }

    private static void ValidateCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new CityIsRequired();
        }
        if (city.Length > CityNameMaxLength)
        {
            throw new CityNameTooLong(CityNameMaxLength);
        }
    }
}
