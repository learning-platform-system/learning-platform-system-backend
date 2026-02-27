using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatsformSystem.Domain.Tests.Shared.DomainValidation;

public class DomainValidatorTests
{
    [Fact]
    public void ValidateRequiredString_ShouldThrowDomainException_WhenValueIsEmpty()
    {
        // Arrange
        string? value = " ";
        int maxLength = 10;
        string requiredErrorMessage = "Value is required";
        string tooLongErrorMessage = "Value is too long";

        // Act
        DomainException? exception = Assert.Throws<DomainException>(() =>
            DomainValidator.ValidateRequiredString(
                value,
                maxLength,
                requiredErrorMessage,
                tooLongErrorMessage));

        // Assert
        Assert.Equal(requiredErrorMessage, exception.Message);
    }

    [Fact]
    public void ValidateOptionalString_ShouldThrowDomainException_WhenTooLong()
    {
        // Arrange
        string value = "This text is too long";
        int maxLength = 5;
        string tooLongErrorMessage = "Value is too long";

        // Act
        DomainException? exception = Assert.Throws<DomainException>(() =>
            DomainValidator.ValidateOptionalString(
                value,
                maxLength,
                tooLongErrorMessage));

        // Assert
        Assert.Equal(tooLongErrorMessage, exception.Message);
    }

    [Fact]
    public void ValidateRequiredStringWithLengthRange_ShouldThrowDomainException_WhenTooShort()
    {
        // Arrange
        string value = "A";
        int minLength = 2;
        int maxLength = 10;
        string requiredErrorMessage = "Value is required";
        string tooShortErrorMessage = "Value is too short";
        string tooLongErrorMessage = "Value is too long";

        // Act
        DomainException? exception = Assert.Throws<DomainException>(() =>
            DomainValidator.ValidateRequiredStringWithLengthRange(
                value,
                minLength,
                maxLength,
                requiredErrorMessage,
                tooShortErrorMessage,
                tooLongErrorMessage));

        // Assert
        Assert.Equal(tooShortErrorMessage, exception.Message);
    }

    [Fact]
    public void ValidateRequiredGuid_ShouldThrowDomainException_WhenGuidIsEmpty()
    {
        // Arrange
        Guid id = Guid.Empty;
        string errorMessage = "Id is required";

        // Act
        DomainException? exception = Assert.Throws<DomainException>(() =>
            DomainValidator.ValidateRequiredGuid(
                id,
                errorMessage));

        // Assert
        Assert.Equal(errorMessage, exception.Message);
    }

    [Fact]
    public void ValidateOptionalString_Overload_ShouldReturnNull_WhenWhitespace()
    {
        // Arrange
        string value = "   ";

        // Act
        string? result = DomainValidator.ValidateOptionalString(value);

        // Assert
        Assert.Null(result);
    }
}
