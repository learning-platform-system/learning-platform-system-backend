using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;

namespace LearningPlatsformSystem.Domain.Tests.Shared.ValueObjects;

public class ContactInformationTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenEmailIsEmpty()
    {
        // Arrange
        string email = " ";
        string phoneNumber = "12345678";

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            ContactInformation.Create(email, phoneNumber));

        // Assert
        Assert.Equal(
            ContactInformationErrors.EmailIsRequired,
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenPhoneNumberIsTooShort()
    {
        // Arrange
        string email = "test@email.com";
        string phoneNumber = "123";

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            ContactInformation.Create(email, phoneNumber));

        // Assert
        Assert.Equal(
            ContactInformationErrors.PhoneNumberIsTooShort(ContactInformation.PhoneNumberMinLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenPhoneNumberIsTooLong()
    {
        // Arrange
        string email = "test@email.com";
        string phoneNumber = new string('1', ContactInformation.PhoneNumberMaxLength + 1);

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            ContactInformation.Create(email, phoneNumber));

        // Assert
        Assert.Equal(
            ContactInformationErrors.PhoneNumberIsTooLong(ContactInformation.PhoneNumberMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldCreateContactInformation_WhenInputIsValid()
    {
        // Arrange
        string email = "test@email.com";
        string phoneNumber = "12345678";

        // Act
        ContactInformation contactInformation =
            ContactInformation.Create(email, phoneNumber);

        // Assert
        Assert.Equal("test@email.com", contactInformation.Email);
        Assert.Equal("12345678", contactInformation.PhoneNumber);
    }

    [Fact]
    public void TwoContactInformations_ShouldBeEqual_WhenValuesAreSame()
    {
        // Arrange
        ContactInformation first =
            ContactInformation.Create("test@email.com", "12345678");

        ContactInformation second =
            ContactInformation.Create("test@email.com", "12345678");

        // Act
        bool areEqual = first.Equals(second);

        // Assert
        Assert.True(areEqual);
    }
}
