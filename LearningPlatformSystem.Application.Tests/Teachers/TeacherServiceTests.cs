using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers;
using LearningPlatformSystem.Application.Teachers.Inputs;
using LearningPlatformSystem.Application.Teachers.Outputs;
using LearningPlatformSystem.Domain.Teachers;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Teachers;

public class TeacherServiceTests : TeacherServiceTestBase
{
    [Fact]
    public async Task AddTeacherAddressAsync_Should_Return_NotFound_When_Teacher_Does_Not_Exist()
    {
        // Arrange
        Guid teacherId = Guid.NewGuid();

        AddTeacherAddressInput input = new AddTeacherAddressInput
        (
            Id: teacherId,
            Street: "Testgatan 1",
            PostalCode: "12345",
            City: "Stockholm"
        );

        TeacherRepositoryMock
            .Setup(repository => repository.GetByIdAsync(teacherId, CancellationToken))
            .ReturnsAsync((Teacher?)null);

        // Act
        ApplicationResult result =
            await Service.AddTeacherAddressAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            TeacherApplicationErrors.NotFound(teacherId).Message,
            result.Error!.Message
        );

        TeacherRepositoryMock.Verify(
            repository => repository.UpdateAsync(It.IsAny<Teacher>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task CreateTeacherAsync_Should_Return_EmailAlreadyExists_When_Email_Exists()
    {
        // Arrange
        string email = "teacher@test.se";

        CreateTeacherInput input = new CreateTeacherInput
        (
            FirstName: "Eva",
            LastName: "Eriksson",
            Email: email,
            PhoneNumber: "123456"
        );

        TeacherRepositoryMock
            .Setup(repository => repository.ExistsWithTheSameEmailAsync(email, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateTeacherAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            TeacherApplicationErrors.EmailAlreadyExists(email).Message,
            result.Error!.Message
        );

        TeacherRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Teacher>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteTeacherAsync_Should_Return_NotFound_When_Teacher_Does_Not_Exist()
    {
        // Arrange
        Guid teacherId = Guid.NewGuid();

        TeacherRepositoryMock
            .Setup(repository => repository.RemoveAsync(teacherId, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.DeleteTeacherAsync(teacherId, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            TeacherApplicationErrors.NotFound(teacherId).Message,
            result.Error!.Message
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task GetAllTeachersAsync_Should_Return_List_Of_TeacherOutputs()
    {
        // Arrange
        Teacher teacher = Teacher.Create(
            "Eva",
            "Eriksson",
            "eva@test.se",
            "12345678"
        );

        IReadOnlyList<Teacher> teachers = new List<Teacher> { teacher };

        TeacherRepositoryMock
            .Setup(repository => repository.GetAllAsync(CancellationToken))
            .ReturnsAsync(teachers);

        // Act
        ApplicationResult<IReadOnlyList<TeacherOutput>> result =
            await Service.GetAllTeachersAsync(CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
        Assert.Equal(teacher.Id, result.Data![0].Id);
        Assert.Equal(teacher.Name.FirstName, result.Data![0].FirstName);
        Assert.Equal(teacher.ContactInformation.Email, result.Data![0].Email);
    }
}
