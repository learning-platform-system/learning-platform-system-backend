using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;

namespace LearningPlatformSystem.Domain.Tests.Campuses;

public class CampusTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsNull()
    {
        // Arrange
        string name = null!;

        // Act
        Action act = () =>
            Campus.Create(name, "Street 1", "12345", "Stockholm");

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CampusErrors.CampusNameIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsTooLong()
    {
        // Arrange
        string name = new string('A', Campus.CampusNameMaxLength + 1);

        // Act
        Action act = () =>
            Campus.Create(name, "Street 1", "12345", "Stockholm");

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CampusErrors.CampusNameIsTooLong(Campus.CampusNameMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldCreateCampus_WhenValid()
    {
        // Arrange
        string name = "Main Campus";

        // Act
        Campus campus =
            Campus.Create(name, "Street 1", "12345", "Stockholm");

        // Assert
        Assert.Equal(name, campus.Name);
        Assert.NotNull(campus.Address);
        Assert.Null(campus.ContactInformation);
    }

    [Fact]
    public void ChangeName_ShouldThrowDomainException_WhenNameIsInvalid()
    {
        // Arrange
        Campus campus =
            Campus.Create("Main", "Street 1", "12345", "Stockholm");

        // Act
        Action act = () => campus.ChangeName(null!);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CampusErrors.CampusNameIsRequired, exception.Message);
    }

    [Fact]
    public void ChangeName_ShouldUpdateName_WhenValid()
    {
        // Arrange
        Campus campus =
            Campus.Create("Main", "Street 1", "12345", "Stockholm");

        // Act
        campus.ChangeName("NewName");

        // Assert
        Assert.Equal("NewName", campus.Name);
    }

    [Fact]
    public void ChangeAddress_ShouldUpdateAddress_WhenValid()
    {
        // Arrange
        Campus campus =
            Campus.Create("Main", "Street 1", "12345", "Stockholm");

        // Act
        campus.ChangeAddress("Street 2", "54321", "Göteborg");

        // Assert
        Assert.Equal("Street 2", campus.Address.Street);
        Assert.Equal("54321", campus.Address.PostalCode);
        Assert.Equal("Göteborg", campus.Address.City);
    }

    [Fact]
    public void AddContactInformation_ShouldAddContactInformation_WhenNotExists()
    {
        // Arrange
        Campus campus =
            Campus.Create("Main", "Street 1", "12345", "Stockholm");

        // Act
        campus.AddContactInformation("test@test.se", "0700000000");

        // Assert
        Assert.NotNull(campus.ContactInformation);
    }

    [Fact]
    public void AddContactInformation_ShouldThrowDomainException_WhenAlreadyExists()
    {
        // Arrange
        Campus campus =
            Campus.Create("Main", "Street 1", "12345", "Stockholm");

        campus.AddContactInformation("test@test.se", "0700000000");

        // Act
        Action act =
            () => campus.AddContactInformation("new@test.se", "0711111111");

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CampusErrors.ContactInformationAlreadyExists, exception.Message);
    }

    [Fact]
    public void ChangeContactInformation_ShouldThrowDomainException_WhenNotExists()
    {
        // Arrange
        Campus campus =
            Campus.Create("Main", "Street 1", "12345", "Stockholm");

        // Act
        Action act =
            () => campus.ChangeContactInformation("test@test.se", "0700000000");

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CampusErrors.ContactInformationIsRequired, exception.Message);
    }

    [Fact]
    public void ChangeContactInformation_ShouldUpdateContactInformation_WhenExists()
    {
        // Arrange
        Campus campus =
            Campus.Create("Main", "Street 1", "12345", "Stockholm");

        campus.AddContactInformation("test@test.se", "0700000000");

        // Act
        campus.ChangeContactInformation("new@test.se", "0711111111");

        // Assert
        Assert.Equal("new@test.se", campus.ContactInformation!.Email);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Address address =
            Address.Create("Street 1", "12345", "Stockholm");

        ContactInformation contact =
            ContactInformation.Create("test@test.se", "0700000000");

        // Act
        Campus campus =
            Campus.Rehydrate(id, "Main", address, contact);

        // Assert
        Assert.Equal(id, campus.Id);
        Assert.Equal("Main", campus.Name);
        Assert.Equal(address, campus.Address);
        Assert.Equal(contact, campus.ContactInformation);
    }
}
