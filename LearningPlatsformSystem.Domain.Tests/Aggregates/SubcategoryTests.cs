using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Domain.Tests.Aggregates;

public class SubcategoryTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenCategoryIdIsEmpty()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid categoryId = Guid.Empty;
        string name = "Backend";

        // Act
        Action act = () => Subcategory.Create(id, categoryId, name);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(SubcategoryErrors.CategoryIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid categoryId = Guid.NewGuid();
        string name = null!;

        // Act
        Action act = () => Subcategory.Create(id, categoryId, name);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(SubcategoryErrors.NameIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsTooLong()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid categoryId = Guid.NewGuid();
        string name = new string('A', Subcategory.NameMaxLength + 1);

        // Act
        Action act = () => Subcategory.Create(id, categoryId, name);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            SubcategoryErrors.SubcategoryNameIsTooLong(Subcategory.NameMaxLength),
            exception.Message);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid categoryId = Guid.NewGuid();
        string name = "Databaser";

        // Act
        Subcategory subcategory = Subcategory.Rehydrate(id, categoryId, name);

        // Assert
        Assert.Equal(id, subcategory.Id);
        Assert.Equal(categoryId, subcategory.CategoryId);
        Assert.Equal(name, subcategory.Name);
    }
}
