using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.Classrooms.Inputs;
using LearningPlatformSystem.Application.Classrooms.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Classrooms;

public class ClassroomServiceTests : ClassroomServiceTestBase
{
    [Fact]
    public async Task CreateClassroomAsync_Should_Return_NameAlreadyExists_When_Name_Exists()
    {
        // Arrange
        string name = "A101";

        CreateClassroomInput input =
            new CreateClassroomInput(name, 20, ClassroomType.SeminarRoom);

        ClassroomRepositoryMock
            .Setup(r => r.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateClassroomAsync(input, CancellationToken);

        // Assert
        ApplicationResultError expected =
            ClassroomApplicationErrors.NameAlreadyExists(name);

        Assert.False(result.IsSuccess);
        Assert.Equal(expected.Type, result.Error!.Type);
        Assert.Equal(expected.Message, result.Error!.Message);

        ClassroomRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Classroom>(), CancellationToken),
            Times.Never);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Never);
    }

    [Fact]
    public async Task CreateClassroomAsync_Should_Add_And_Save_When_Name_Does_Not_Exist()
    {
        // Arrange
        string name = "B202";

        CreateClassroomInput input =
            new CreateClassroomInput(name, 30, ClassroomType.SeminarRoom);

        ClassroomRepositoryMock
            .Setup(r => r.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateClassroomAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);

        ClassroomRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Classroom>(), CancellationToken),
            Times.Once);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task DeleteClassroomAsync_Should_Return_NotFound_When_Classroom_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        ClassroomRepositoryMock
            .Setup(r => r.RemoveAsync(id, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.DeleteClassroomAsync(id, CancellationToken);

        // Assert
        ApplicationResultError expected =
            ClassroomApplicationErrors.CouldNotBeFound(id);

        Assert.False(result.IsSuccess);
        Assert.Equal(expected.Type, result.Error!.Type);
        Assert.Equal(expected.Message, result.Error!.Message);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Never);
    }

    [Fact]
    public async Task DeleteClassroomAsync_Should_Remove_And_Save_When_Classroom_Exists()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        ClassroomRepositoryMock
            .Setup(r => r.RemoveAsync(id, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult result =
            await Service.DeleteClassroomAsync(id, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task GetClassroomByTypeAsync_Should_Return_Mapped_ClassroomOutputs()
    {
        // Arrange
        Classroom classroom =
            Classroom.Create(Guid.NewGuid(), "C303", 25, ClassroomType.SeminarRoom);

        IReadOnlyList<Classroom> classrooms =
            new List<Classroom> { classroom };

        ClassroomRepositoryMock
            .Setup(r => r.GetByTypeAsync(ClassroomType.SeminarRoom, CancellationToken))
            .ReturnsAsync(classrooms);

        // Act
        ApplicationResult<IReadOnlyList<ClassroomOutput>> result =
            await Service.GetClassroomByTypeAsync(ClassroomType.SeminarRoom, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
        Assert.Equal("C303", result.Data![0].Name);
        Assert.Equal(25, result.Data[0].Capacity);
    }

    [Fact]
    public async Task UpdateClassroomAsync_Should_Return_NotFound_When_Classroom_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        UpdateClassroomInput input =
            new UpdateClassroomInput(id, "D404", 40, ClassroomType.SeminarRoom);

        ClassroomRepositoryMock
            .Setup(r => r.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync((Classroom?)null);

        // Act
        ApplicationResult result =
            await Service.UpdateClassroomAsync(input, CancellationToken);

        // Assert
        ApplicationResultError expected =
            ClassroomApplicationErrors.CouldNotBeFound(id);

        Assert.False(result.IsSuccess);
        Assert.Equal(expected.Type, result.Error!.Type);
        Assert.Equal(expected.Message, result.Error!.Message);
    }

    [Fact]
    public async Task UpdateClassroomAsync_Should_Return_NameAlreadyExists_When_Name_Is_Duplicate()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        Classroom classroom =
            Classroom.Create(id, "A101", 20, ClassroomType.SeminarRoom);

        string newName = "B202";

        UpdateClassroomInput input =
            new UpdateClassroomInput(id, newName, 50, ClassroomType.SeminarRoom);

        ClassroomRepositoryMock
            .Setup(r => r.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(classroom);

        ClassroomRepositoryMock
            .Setup(r => r.ExistsAnotherWithSameNameAsync(newName, id, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult result =
            await Service.UpdateClassroomAsync(input, CancellationToken);

        // Assert
        ApplicationResultError expected =
            ClassroomApplicationErrors.NameAlreadyExists(newName);

        Assert.False(result.IsSuccess);
        Assert.Equal(expected.Type, result.Error!.Type);
        Assert.Equal(expected.Message, result.Error!.Message);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Never);
    }

    [Fact]
    public async Task UpdateClassroomAsync_Should_Update_And_Save_When_Valid()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        Classroom classroom =
            Classroom.Create(id, "A101", 20, ClassroomType.SeminarRoom);

        string newName = "B202";

        UpdateClassroomInput input =
            new UpdateClassroomInput(id, newName, 50, ClassroomType.SeminarRoom);

        ClassroomRepositoryMock
            .Setup(r => r.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(classroom);

        ClassroomRepositoryMock
            .Setup(r => r.ExistsAnotherWithSameNameAsync(newName, id, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.UpdateClassroomAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        ClassroomRepositoryMock.Verify(
            r => r.UpdateAsync(classroom, CancellationToken),
            Times.Once);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Once);
    }
}