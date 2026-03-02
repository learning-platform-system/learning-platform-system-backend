using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Domain.Tests.Categories;

public class CategoryTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = null!;

        // Act
        Action act = () => Category.Create(id, name);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CategoryErrors.NameIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenNameIsTooLong()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = new string('A', Category.NameMaxLength + 1);

        // Act
        Action act = () => Category.Create(id, name);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CategoryErrors.NameIsTooLong(Category.NameMaxLength),
            exception.Message);
    }

    [Fact]
    public void Create_ShouldCreateCategory_WhenValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Backend";

        // Act
        Category category = Category.Create(id, name);

        // Assert
        Assert.Equal(id, category.Id);
        Assert.Equal(name, category.Name);
        Assert.Empty(category.Subcategories);
    }

    [Fact]
    public void ChangeName_ShouldThrowDomainException_WhenNameIsInvalid()
    {
        // Arrange
        Category category = Category.Create(Guid.NewGuid(), "Backend");

        // Act
        Action act = () => category.ChangeName(null!);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CategoryErrors.NameIsRequired, exception.Message);
    }

    [Fact]
    public void ChangeName_ShouldUpdateName_WhenValid()
    {
        // Arrange
        Category category = Category.Create(Guid.NewGuid(), "Backend");

        // Act
        category.ChangeName("Frontend");

        // Assert
        Assert.Equal("Frontend", category.Name);
    }

    [Fact]
    public void AddSubcategory_ShouldAddSubcategory()
    {
        // Arrange
        Category category = Category.Create(Guid.NewGuid(), "Backend");

        // Act
        category.AddSubcategory("API");

        // Assert
        Assert.Single(category.Subcategories);
    }

    [Fact]
    public void EnsureCanBeRemoved_ShouldThrowDomainException_WhenSubcategoriesExist()
    {
        // Arrange
        Category category = Category.Create(Guid.NewGuid(), "Backend");
        category.AddSubcategory("API");

        // Act
        Action act = () => category.EnsureCanBeRemoved();

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CategoryErrors.CannotBeRemoved, exception.Message);
    }

    [Fact]
    public void EnsureCanBeRemoved_ShouldNotThrow_WhenNoSubcategoriesExist()
    {
        // Arrange
        Category category = Category.Create(Guid.NewGuid(), "Backend");

        // Act
        Exception? exception = Record.Exception(() => category.EnsureCanBeRemoved());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithSubcategories()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Backend";
        Subcategory subcategory =
            Subcategory.Rehydrate(Guid.NewGuid(), id, "API");

        // Act
        Category category =
            Category.Rehydrate(id, name, new[] { subcategory });

        // Assert
        Assert.Equal(id, category.Id);
        Assert.Equal(name, category.Name);
        Assert.Single(category.Subcategories);
    }
}
