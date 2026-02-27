using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatsformSystem.Domain.Tests.DomainValidation;

public class DomainValidatorTests
{
    [Fact]
    public void ValidateRequiredString_ShouldThrow_WhenValueIsNullOrEmpty()
    {
        // Arrange
        string? value = " ";
        int maxLength = 10;

        // Act
        Action act = () => DomainValidator.ValidateRequiredString(
            value,
            maxLength,
            "Value is required",
            "Value is too long");

        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Fact]
    public void ValidateOptionalString_ShouldThrow_WhenTooLong()
    {
        // Arrange
        string value = "This string is definitely too long";
        int maxLength = 5;

        // Act
        Action act = () => DomainValidator.ValidateOptionalString(
            value,
            maxLength,
            "Too long");

        // Assert
        Assert.Throws<DomainException>(act);
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

    [Fact]
    public void ValidateRequiredStringWithLengthRange_ShouldThrow_WhenTooShort()
    {
        // Arrange
        string value = "A";
        int minLength = 2;
        int maxLength = 10;

        // Act
        Action act = () => DomainValidator.ValidateRequiredStringWithLengthRange(
            value,
            minLength,
            maxLength,
            "Required",
            "Too short",
            "Too long");

        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Fact]
    public void ValidateRequiredGuid_ShouldThrow_WhenGuidIsEmpty()
    {
        // Arrange
        Guid id = Guid.Empty;

        // Act
        Action act = () => DomainValidator.ValidateRequiredGuid(
            id,
            "Id is required");

        // Assert
        Assert.Throws<DomainException>(act);
    }
}
