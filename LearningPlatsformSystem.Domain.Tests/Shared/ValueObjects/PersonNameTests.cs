using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;

namespace LearningPlatsformSystem.Domain.Tests.Shared.ValueObjects;

public class PersonNameTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenFirstNameIsEmpty()
    {
        // Arrange
        string firstName = " ";
        string lastName = "Andersson";

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            PersonName.Create(firstName, lastName));

        // Assert
        Assert.Equal(
            PersonNameErrors.FirstNameIsRequired,
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenLastNameIsTooLong()
    {
        // Arrange
        string firstName = "Anna";
        string lastName = new string('A', PersonName.LastNameMaxLength + 1);

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            PersonName.Create(firstName, lastName));

        // Assert
        Assert.Equal(
            PersonNameErrors.LastNameIsTooLong(PersonName.LastNameMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldCreatePersonName_WhenInputIsValid()
    {
        // Arrange
        string firstName = "Anna";
        string lastName = "Andersson";

        // Act
        PersonName personName = PersonName.Create(firstName, lastName);

        // Assert
        Assert.Equal("Anna", personName.FirstName);
        Assert.Equal("Andersson", personName.LastName);
    }
}

