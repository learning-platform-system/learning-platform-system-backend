using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.ValueObjects.Addresses;
using LearningPlatformSystem.Domain.Shared.ValueObjects.ContactInformations;
using LearningPlatformSystem.Domain.Shared.ValueObjects.PersonNames;
using LearningPlatformSystem.Domain.Teachers;

namespace LearningPlatformSystem.Domain.Tests.Teachers;

public class TeacherTests
{
    [Fact]
    public void Create_ShouldCreateTeacher_WhenInputIsValid()
    {
        // Arrange
        string firstName = "Anna";
        string lastName = "Andersson";
        string email = "test@email.com";
        string phoneNumber = "12345678";

        // Act
        Teacher teacher = Teacher.Create(firstName, lastName, email, phoneNumber);

        // Assert
        Assert.NotEqual(Guid.Empty, teacher.Id);
        Assert.Equal("Anna", teacher.Name.FirstName);
        Assert.Equal("Andersson", teacher.Name.LastName);
        Assert.Equal("test@email.com", teacher.ContactInformation.Email);
        Assert.Equal("12345678", teacher.ContactInformation.PhoneNumber);
        Assert.Null(teacher.Address);
    }

    [Fact]
    public void Rehydrate_ShouldRestoreTeacher_WhenInputIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        PersonName name = PersonName.Create("Anna", "Andersson");
        ContactInformation contactInformation = ContactInformation.Create("test@email.com", "12345678");

        Address address = Address.Create("Storgatan 1", "12345", "Stockholm");

        // Act
        Teacher teacher = Teacher.Rehydrate(id, name, contactInformation, address);

        // Assert
        Assert.Equal(id, teacher.Id);
        Assert.Equal("Anna", teacher.Name.FirstName);
        Assert.Equal("Andersson", teacher.Name.LastName);
        Assert.Equal("test@email.com", teacher.ContactInformation.Email);
        Assert.Equal("12345678", teacher.ContactInformation.PhoneNumber);
        Assert.NotNull(teacher.Address);
        Assert.Equal("Storgatan 1", teacher.Address!.Street);
        Assert.Equal("12345", teacher.Address.PostalCode);
        Assert.Equal("Stockholm", teacher.Address.City);
    }

    [Fact]
    public void ChangeName_ShouldChangeName_WhenInputIsValid()
    {
        // Arrange
        Teacher teacher = Teacher.Create("Anna", "Andersson", "test@email.com", "12345678");

        // Act
        teacher.ChangeName("Eva", "Svensson");

        // Assert
        Assert.Equal("Eva", teacher.Name.FirstName);
        Assert.Equal("Svensson", teacher.Name.LastName);
    }

    [Fact]
    public void AddAddress_ShouldThrowDomainException_WhenAddressAlreadyExists()
    {
        // Arrange
        Teacher teacher = Teacher.Create("Anna", "Andersson", "test@email.com", "12345678");
        teacher.AddAddress("Storgatan 1", "12345", "Stockholm");

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            teacher.AddAddress("Nygatan 2", "54321", "Uppsala"));

        // Assert
        Assert.Equal(TeacherErrors.AddressAlreadyExists, exception.Message);
    }

    [Fact]
    public void ChangeAddress_ShouldThrowDomainException_WhenAddressIsMissing()
    {
        // Arrange
        Teacher teacher = Teacher.Create("Anna", "Andersson", "test@email.com", "12345678");

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            teacher.ChangeAddress("Storgatan 1", "12345", "Stockholm"));

        // Assert
        Assert.Equal(TeacherErrors.AddressIsRequired, exception.Message);
    }

    [Fact]
    public void ChangeContactInformation_ShouldChangeContactInformation_WhenInputIsValid()
    {
        // Arrange
        Teacher teacher = Teacher.Create("Anna", "Andersson", "test@email.com", "12345678");

        // Act
        teacher.ChangeContactInformation("new@email.com", "87654321");

        // Assert
        Assert.Equal("new@email.com", teacher.ContactInformation.Email);
        Assert.Equal("87654321", teacher.ContactInformation.PhoneNumber);
    }
}
