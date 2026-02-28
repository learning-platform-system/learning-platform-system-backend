using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Categories.Inputs;
using LearningPlatformSystem.Application.Categories.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Categories;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Categories;

public class CategoryServiceTests : CategoryServiceTestBase
{
    [Fact]
    public async Task CreateCategoryAsync_Should_Return_NameAlreadyExists_When_Name_Exists()
    {
        // Arrange
        string name = "Programmering";

        CreateCategoryInput input = new CreateCategoryInput(name);

        CategoryRepositoryMock
            .Setup(repository => repository.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCategoryAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CategoryApplicationErrors.NameAlreadyExists(name).Message,
            result.Error!.Message
        );

        CategoryRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Category>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task CreateCategoryAsync_Should_Add_And_Save_When_Name_Does_Not_Exist()
    {
        // Arrange
        string name = "Design";

        CreateCategoryInput input = new CreateCategoryInput(name);

        CategoryRepositoryMock
            .Setup(repository => repository.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCategoryAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);

        CategoryRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Category>(), CancellationToken),
            Times.Once
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task GetCategoryByIdAsync_Should_Return_NotFound_When_Category_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync((Category?)null);

        // Act
        ApplicationResult<CategoryOutput> result =
            await Service.GetCategoryByIdAsync(id, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CategoryApplicationErrors.NotFound(id).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task GetCategoryByIdAsync_Should_Return_CategoryOutput_When_Category_Exists()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Category category = Category.Create(id, "Backend");

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(category);

        // Act
        ApplicationResult<CategoryOutput> result =
            await Service.GetCategoryByIdAsync(id, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Backend", result.Data!.Name);
        Assert.Equal(id, result.Data.Id);
    }

    [Fact]
    public async Task DeleteCategoryAsync_Should_Return_NotFound_When_Category_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync((Category?)null);

        // Act
        ApplicationResult result =
            await Service.DeleteCategoryAsync(id, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CategoryApplicationErrors.NotFound(id).Message,
            result.Error!.Message
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteCategoryAsync_Should_Remove_And_Save_When_Category_Exists()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Category category = Category.Create(id, "Frontend");

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(category);

        // Act
        ApplicationResult result =
            await Service.DeleteCategoryAsync(id, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        CategoryRepositoryMock.Verify(
            repository => repository.RemoveAsync(id, CancellationToken),
            Times.Once
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateCategoryNameAsync_Should_Return_NotFound_When_Category_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        UpdateCategoryNameInput input =
            new UpdateCategoryNameInput(id, "NewName");

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync((Category?)null);

        // Act
        ApplicationResult result =
            await Service.UpdateCategoryNameAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CategoryApplicationErrors.NotFound(id).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task UpdateCategoryNameAsync_Should_Return_NameAlreadyExists_When_Name_Is_Duplicate()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string newName = "Duplicate";

        Category category = Category.Create(id, "OldName");

        UpdateCategoryNameInput input =
            new UpdateCategoryNameInput(id, newName);

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(category);

        CategoryRepositoryMock
            .Setup(repository => repository.ExistsAnotherWithSameNameAsync(newName, id, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult result =
            await Service.UpdateCategoryNameAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CategoryApplicationErrors.NameAlreadyExists(newName).Message,
            result.Error!.Message
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task UpdateCategoryNameAsync_Should_Update_And_Save_When_Valid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string newName = "NewValidName";

        Category category = Category.Create(id, "OldName");

        UpdateCategoryNameInput input =
            new UpdateCategoryNameInput(id, newName);

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(category);

        CategoryRepositoryMock
            .Setup(repository => repository.ExistsAnotherWithSameNameAsync(newName, id, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.UpdateCategoryNameAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        CategoryRepositoryMock.Verify(
            repository => repository.UpdateAsync(category, CancellationToken),
            Times.Once
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task AddSubcategoryAsync_Should_Return_NotFound_When_Category_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        AddSubcategoryInput input =
            new AddSubcategoryInput(id, "Sub");

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync((Category?)null);

        // Act
        ApplicationResult result =
            await Service.AddSubcategoryAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CategoryApplicationErrors.NotFound(id).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task AddSubcategoryAsync_Should_Update_And_Save_When_Category_Exists()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Category category = Category.Create(id, "Backend");

        AddSubcategoryInput input =
            new AddSubcategoryInput(id, "API");

        CategoryRepositoryMock
            .Setup(repository => repository.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(category);

        // Act
        ApplicationResult result =
            await Service.AddSubcategoryAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        CategoryRepositoryMock.Verify(
            repository => repository.UpdateAsync(category, CancellationToken),
            Times.Once
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Once
        );
    }
}