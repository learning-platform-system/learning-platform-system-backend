using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.CoursePeriodResources;

public class CoursePeriodResourceTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenCoursePeriodIdIsEmpty()
    {
        // Arrange
        Guid coursePeriodId = Guid.Empty;
        string title = "Material";
        string url = "https://test.se";
        string? description = "Beskrivning";

        // Act
        Action act = () => CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodResourceErrors.CoursePeriodIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenTitleIsNull()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        string title = null!;
        string url = "https://test.se";
        string? description = "Beskrivning";

        // Act
        Action act = () => CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodResourceErrors.CoursePeriodResourceTitleIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenTitleIsTooLong()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        string title = new string('A', CoursePeriodResource.TitleMaxLength + 1);
        string url = "https://test.se";
        string? description = "Beskrivning";

        // Act
        Action act = () => CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CoursePeriodResourceErrors.CoursePeriodResourceTitleIsTooLong(CoursePeriodResource.TitleMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenDescriptionIsTooLong()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        string title = "Material";
        string url = "https://test.se";
        string? description = new string('A', CoursePeriodResource.DescriptionMaxLength + 1);

        // Act
        Action act = () => CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CoursePeriodResourceErrors.CoursePeriodResourceDescriptionIsTooLong(CoursePeriodResource.DescriptionMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenUrlIsNull()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        string title = "Material";
        string url = null!;
        string? description = "Beskrivning";

        // Act
        Action act = () => CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodResourceErrors.CoursePeriodResourceUrlIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenUrlIsTooLong()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        string title = "Material";
        string url = new string('A', CoursePeriodResource.UrlMaxLength + 1);
        string? description = "Beskrivning";

        // Act
        Action act = () => CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CoursePeriodResourceErrors.CoursePeriodResourceUrlIsTooLong(CoursePeriodResource.UrlMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenUrlIsInvalid()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        string title = "Material";
        string url = "inte-en-url";
        string? description = "Beskrivning";

        // Act
        Action act = () => CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CoursePeriodResourceErrors.CoursePeriodResourceUrlIsInvalid,
            exception.Message);
    }

    [Fact]
    public void Create_ShouldCreateResource_WhenValid()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        string title = "Material";
        string url = "https://test.se";
        string? description = "Beskrivning";

        // Act
        CoursePeriodResource resource = CoursePeriodResource.Create(coursePeriodId, title, url, description);

        // Assert
        Assert.Equal(coursePeriodId, resource.CoursePeriodId);
        Assert.Equal(title, resource.Title);
        Assert.Equal(url, resource.Url);
        Assert.Equal(description, resource.Description);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();
        string title = "Material";
        string url = "https://test.se";
        string? description = "Beskrivning";

        // Act
        CoursePeriodResource resource = CoursePeriodResource.Rehydrate(id, coursePeriodId, title, url, description);

        // Assert
        Assert.Equal(id, resource.Id);
        Assert.Equal(coursePeriodId, resource.CoursePeriodId);
        Assert.Equal(title, resource.Title);
        Assert.Equal(url, resource.Url);
        Assert.Equal(description, resource.Description);
    }
}
