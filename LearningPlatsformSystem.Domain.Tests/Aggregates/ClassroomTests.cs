using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.Aggregates;

public class ClassroomTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenIdIsEmpty()
    {
        // Arrange
        Guid id = Guid.Empty;
        string name = "A1";
        int capacity = 20;
        ClassroomType type = ClassroomType.LectureHall;

        // Act
        Action act = () => Classroom.Create(id, name, capacity, type);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(ClassroomErrors.IdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = null!;
        int capacity = 20;
        ClassroomType type = ClassroomType.LectureHall;

        // Act
        Action act = () => Classroom.Create(id, name, capacity, type);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(ClassroomErrors.NameIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsTooLong()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = new string('A', Classroom.NameMaxLength + 1);
        int capacity = 20;
        ClassroomType type = ClassroomType.LectureHall;

        // Act
        Action act = () => Classroom.Create(id, name, capacity, type);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            ClassroomErrors.NameIsTooLong(Classroom.NameMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenCapacityIsZeroOrNegative()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "A1";
        int capacity = 0;
        ClassroomType type = ClassroomType.LectureHall;

        // Act
        Action act = () => Classroom.Create(id, name, capacity, type);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(ClassroomErrors.CapacityMustBePositive, exception.Message);
    }

    [Fact]
    public void Create_ShouldCreateClassroom_WhenValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "A1";
        int capacity = 20;
        ClassroomType type = ClassroomType.LectureHall;

        // Act
        Classroom classroom = Classroom.Create(id, name, capacity, type);

        // Assert
        Assert.Equal(id, classroom.Id);
        Assert.Equal(name, classroom.Name);
        Assert.Equal(capacity, classroom.Capacity);
        Assert.Equal(type, classroom.Type);
    }

    [Fact]
    public void Update_ShouldThrowDomainException_WhenNameIsNull()
    {
        // Arrange
        Classroom classroom = Classroom.Create(
            Guid.NewGuid(),
            "A1",
            20,
            ClassroomType.LectureHall);

        string name = null!;
        int capacity = 20;
        ClassroomType type = ClassroomType.LectureHall;

        // Act
        Action act = () => classroom.Update(name, capacity, type);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(ClassroomErrors.NameIsRequired, exception.Message);
    }

    [Fact]
    public void Update_ShouldThrowDomainException_WhenCapacityIsInvalid()
    {
        // Arrange
        Classroom classroom = Classroom.Create(
            Guid.NewGuid(),
            "A1",
            20,
            ClassroomType.LectureHall);

        // Act
        Action act = () => classroom.Update("B2", 0, ClassroomType.Laboratory);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(ClassroomErrors.CapacityMustBePositive, exception.Message);
    }

    [Fact]
    public void Update_ShouldUpdateValues_WhenValid()
    {
        // Arrange
        Classroom classroom = Classroom.Create(
            Guid.NewGuid(),
            "A1",
            20,
            ClassroomType.LectureHall);

        // Act
        classroom.Update("B2", 30, ClassroomType.Laboratory);

        // Assert
        Assert.Equal("B2", classroom.Name);
        Assert.Equal(30, classroom.Capacity);
        Assert.Equal(ClassroomType.Laboratory, classroom.Type);
    }
}
