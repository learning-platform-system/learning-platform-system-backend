using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;


namespace LearningPlatsformSystem.Domain.Tests.Shared.ValueObjects;

public class AddressTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenStreetIsEmpty()
    {
        // Arrange
        string street = " ";
        string postalCode = "12345";
        string city = "Stockholm";

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            Address.Create(street, postalCode, city));

        // Assert
        Assert.Equal(
            AddressErrors.StreetIsRequired,
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenPostalCodeIsTooLong()
    {
        // Arrange
        string street = "Storgatan 1";
        string postalCode = new string('1', Address.PostalCodeMaxLength + 1);
        string city = "Stockholm";

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            Address.Create(street, postalCode, city));

        // Assert
        Assert.Equal(
            AddressErrors.PostalCodeIsTooLong(Address.PostalCodeMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenCityIsEmpty()
    {
        // Arrange
        string street = "Storgatan 1";
        string postalCode = "12345";
        string city = " ";

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            Address.Create(street, postalCode, city));

        // Assert
        Assert.Equal(
            AddressErrors.CityIsRequired,
            exception.Message);
    }

    [Fact]
    public void Create_ShouldCreateAddress_WhenInputIsValid()
    {
        // Arrange
        string street = "Storgatan 1";
        string postalCode = "12345";
        string city = "Stockholm";

        // Act
        Address address = Address.Create(street, postalCode, city);

        // Assert
        Assert.Equal("Storgatan 1", address.Street);
        Assert.Equal("12345", address.PostalCode);
        Assert.Equal("Stockholm", address.City);
    }

    [Fact]
    public void TwoAddresses_ShouldBeEqual_WhenValuesAreSame()
    {
        // Arrange
        Address first =
            Address.Create("Storgatan 1", "12345", "Stockholm");

        Address second =
            Address.Create("Storgatan 1", "12345", "Stockholm");

        // Act
        bool areEqual = first.Equals(second);

        // Assert
        Assert.True(areEqual);
    }
}
