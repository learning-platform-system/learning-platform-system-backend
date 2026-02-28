using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.Courses;

public class CourseTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenTitleIsNull()
    {
        // Arrange
        Guid subcategoryId = Guid.NewGuid();
        string title = null!;
        string? description = "Beskrivning";
        int credits = 5;

        // Act
        Action act = () => Course.Create(subcategoryId, title, description, credits);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseErrors.CourseTitleIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenTitleIsTooLong()
    {
        // Arrange
        Guid subcategoryId = Guid.NewGuid();
        string title = new string('A', Course.CourseTitleMaxLength + 1);
        string? description = "Beskrivning";
        int credits = 5;

        // Act
        Action act = () => Course.Create(subcategoryId, title, description, credits);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CourseErrors.CourseTitleIsTooLong(Course.CourseTitleMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenCreditsAreOutOfRange()
    {
        // Arrange
        Guid subcategoryId = Guid.NewGuid();
        string title = "Backend";
        string? description = "Beskrivning";
        int credits = 0; // under min

        // Act
        Action act = () => Course.Create(subcategoryId, title, description, credits);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CourseErrors.CreditsValueOutOfRange(Course.CreditsMinValue, Course.CreditsMaxValue),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenDescriptionIsTooLong()
    {
        // Arrange
        Guid subcategoryId = Guid.NewGuid();
        string title = "Backend";
        string? description = new string('A', Course.CourseDescriptionMaxLength + 1);
        int credits = 5;

        // Act
        Action act = () => Course.Create(subcategoryId, title, description, credits);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CourseErrors.CourseDescriptionIsTooLong(Course.CourseDescriptionMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenSubcategoryIdIsEmpty()
    {
        // Arrange
        Guid subcategoryId = Guid.Empty;
        string title = "Backend";
        string? description = "Beskrivning";
        int credits = 5;

        // Act
        Action act = () => Course.Create(subcategoryId, title, description, credits);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CourseErrors.SubcategoryIdIsRequired, exception.Message);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid subcategoryId = Guid.NewGuid();
        string title = "Databaser";
        string? description = "SQL och EF Core";
        int credits = 10;

        // Act
        Course course = Course.Rehydrate(id, subcategoryId, title, description, credits);

        // Assert
        Assert.Equal(id, course.Id);
        Assert.Equal(subcategoryId, course.SubcategoryId);
        Assert.Equal(title, course.Title);
        Assert.Equal(description, course.Description);
        Assert.Equal(credits, course.Credits);
    }
}
