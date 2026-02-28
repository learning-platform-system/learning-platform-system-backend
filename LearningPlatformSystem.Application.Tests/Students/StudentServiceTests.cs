using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students;
using LearningPlatformSystem.Application.Students.Inputs;
using LearningPlatformSystem.Application.Students.Outputs;
using LearningPlatformSystem.Domain.Students;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Students;

public class StudentServiceTests : StudentServiceTestBase
{
    [Fact]
    public async Task AddStudentAddressAsync_Should_Return_NotFound_When_Student_Does_Not_Exist()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();

        AddStudentAddressInput input = new AddStudentAddressInput
        (
            Id: studentId,
            Street: "Testgatan 1",
            PostalCode: "12345",
            City: "Stockholm"
        );

        StudentRepositoryMock
            .Setup(repository => repository.GetByIdAsync(studentId, CancellationToken))
            .ReturnsAsync((Student?)null);

        // Act
        ApplicationResult result =
            await Service.AddStudentAddressAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            StudentApplicationErrors.NotFound(studentId).Message,
            result.Error!.Message
        );

        StudentRepositoryMock.Verify(
            repository => repository.UpdateAsync(It.IsAny<Student>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task CreateStudentAsync_Should_Return_EmailAlreadyExists_When_Email_Exists()
    {
        // Arrange
        string email = "test@test.se";

        CreateStudentInput input = new CreateStudentInput
        (
            FirstName: "Anna",
            LastName: "Andersson",
            Email: email,
            PhoneNumber: "123456"
        );

        StudentRepositoryMock
            .Setup(repository => repository.ExistsWithTheSameEmailAsync(email, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateStudentAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            StudentApplicationErrors.EmailAlreadyExists(email).Message,
            result.Error!.Message
        );

        StudentRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Student>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteStudentAsync_Should_Return_NotFound_When_Student_Does_Not_Exist()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();

        StudentRepositoryMock
            .Setup(repository => repository.RemoveAsync(studentId, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.DeleteStudentAsync(studentId, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            StudentApplicationErrors.NotFound(studentId).Message,
            result.Error!.Message
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task GetAllStudentsAsync_Should_Return_List_Of_StudentOutputs()
    {
        // Arrange
        Student student = Student.Create(
            "Anna",
            "Andersson",
            "anna@test.se",
            "12345678"
        );

        IReadOnlyList<Student> students = new List<Student> { student };

        StudentRepositoryMock
            .Setup(repository => repository.GetAllAsync(CancellationToken))
            .ReturnsAsync(students);

        // Act
        ApplicationResult<IReadOnlyList<StudentOutput>> result =
            await Service.GetAllStudentsAsync(CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
        Assert.Equal(student.Id, result.Data![0].Id);
        Assert.Equal(student.Name.FirstName, result.Data![0].FirstName);
    }
}
