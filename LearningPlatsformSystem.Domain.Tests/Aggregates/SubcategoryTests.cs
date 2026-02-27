using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Subcategories;

namespace LearningPlatformSystem.Domain.Tests.Aggregates;

public class SubcategoryTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenCategoryIdIsEmpty()
    {
        // Arrange (förbered)
        Guid id = Guid.NewGuid();
        Guid categoryId = Guid.Empty;
        string name = "Backend";

        // Act (agera/kör)
        Action act = () => Subcategory.Create(id, categoryId, name);

        // Assert (verifiera) - använder DINA errors
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(SubcategoryErrors.CategoryIdIsRequired, exception.Message);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange (förbered)
        Guid id = Guid.NewGuid();
        Guid categoryId = Guid.NewGuid();
        string name = "Databaser";

        // Act (agera/kör)
        Subcategory subcategory = Subcategory.Rehydrate(id, categoryId, name);

        // Assert (verifiera)
        Assert.Equal(id, subcategory.Id);
        Assert.Equal(categoryId, subcategory.CategoryId);
        Assert.Equal(name, subcategory.Name);
    }
}
