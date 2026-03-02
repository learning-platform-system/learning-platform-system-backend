using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Students;

namespace LearningPlatformSystem.Domain.Tests.Students;

public class StudentTests
{
    [Fact]
    public void Create_ShouldCreateStudent_WhenValidInput()
    {
        // Arrange
        string firstName = "Anna";
        string lastName = "Svensson";
        string email = "anna@mail.com";
        string phoneNumber = "0701234567";

        // Act
        Student student = Student.Create(firstName, lastName, email, phoneNumber);

        // Assert
        Assert.NotEqual(Guid.Empty, student.Id);
        Assert.Equal("Anna", student.Name.FirstName);
        Assert.Equal("Svensson", student.Name.LastName);
    }

    [Fact]
    public void ChangeName_ShouldUpdateName_WhenValidInput()
    {
        // Arrange
        Student student = Student.Create("Anna", "Svensson", "anna@mail.com", "0701234567");

        // Act
        student.ChangeName("Lisa", "Karlsson");

        // Assert
        Assert.Equal("Lisa", student.Name.FirstName);
        Assert.Equal("Karlsson", student.Name.LastName);
    }

    [Fact]
    public void AddAddress_ShouldAddAddress_WhenNoAddressExists()
    {
        // Arrange
        Student student = Student.Create("Anna", "Svensson", "anna@mail.com", "0701234567");

        // Act
        student.AddAddress("Storgatan 1", "12345", "Stockholm");

        // Assert
        Assert.NotNull(student.Address);
    }

    [Fact]
    public void AddAddress_ShouldThrow_WhenAddressAlreadyExists()
    {
        // Arrange
        Student student = Student.Create("Anna", "Svensson", "anna@mail.com", "0701234567");
        student.AddAddress("Storgatan 1", "12345", "Stockholm");

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            student.AddAddress("Ny adress 2", "54321", "Göteborg"));

        // Assert
        Assert.Equal(StudentErrors.AddressAlreadyExists, exception.Message);
    }

    [Fact]
    public void ChangeAddress_ShouldThrow_WhenAddressDoesNotExist()
    {
        // Arrange
        Student student = Student.Create("Anna", "Svensson", "anna@mail.com", "0701234567");

        // Act
        DomainException exception = Assert.Throws<DomainException>(() =>
            student.ChangeAddress("Ny gata 1", "11111", "Malmö"));

        // Assert
        Assert.Equal(StudentErrors.AddressIsRequired, exception.Message);
    }

    [Fact]
    public void ChangeContactInformation_ShouldUpdateContactInformation()
    {
        // Arrange
        Student student = Student.Create("Anna", "Svensson", "anna@mail.com", "0701234567");

        // Act
        student.ChangeContactInformation("lisa@mail.com", "0709999999");

        // Assert
        Assert.Equal("lisa@mail.com", student.ContactInformation.Email);
    }
}
