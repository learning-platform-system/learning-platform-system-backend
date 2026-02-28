using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence.Repositories;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class CategoryRepositoryTests(SqliteInMemoryFixture _fixture)
{
    [Fact]
    public async Task AddAsync_Should_Add_Category()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CategoryRepository repository = new(context);

        Category category =
            Category.Create(Guid.NewGuid(), "Programming");

        // Act
        await repository.AddAsync(category, ct);
        await context.SaveChangesAsync(ct);

        CategoryEntity? entity =
            await context.Categories.SingleOrDefaultAsync(x => x.Id == category.Id, ct);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal("Programming", entity!.Name);
    }

    [Fact]
    public async Task ExistsByNameAsync_Should_Return_True_When_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CategoryRepository repository = new(context);

        Category category =
            Category.Create(Guid.NewGuid(), "Design");

        await repository.AddAsync(category, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool result = await repository.ExistsByNameAsync("Design", ct);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAnotherWithSameNameAsync_Should_Return_True_When_Other_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CategoryRepository repository = new(context);

        Guid existingId = Guid.NewGuid();

        Category existingCategory =
            Category.Create(existingId, "Backend");

        await repository.AddAsync(existingCategory, ct);
        await context.SaveChangesAsync(ct);

        Guid differentId = Guid.NewGuid();

        // Act
        bool result =
            await repository.ExistsAnotherWithSameNameAsync(
                "Backend",
                differentId,
                ct);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Category_With_Subcategories()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CategoryRepository repository = new(context);

        Guid categoryId = Guid.NewGuid();

        Category category =
            Category.Create(categoryId, "Frontend");

        category.AddSubcategory("React");

        await repository.AddAsync(category, ct);
        await context.SaveChangesAsync(ct);

        await repository.UpdateAsync(category, ct);
        await context.SaveChangesAsync(ct);

        // Act
        Category? result = await repository.GetByIdAsync(categoryId, ct);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result!.Subcategories);
    }

    [Fact]
    public async Task RemoveAsync_Should_Remove_Category_When_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CategoryRepository repository = new(context);

        Category category =
            Category.Create(Guid.NewGuid(), "DevOps");

        await repository.AddAsync(category, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool removed = await repository.RemoveAsync(category.Id, ct);
        await context.SaveChangesAsync(ct);

        bool exists =
            await context.Categories.AnyAsync(x => x.Id == category.Id, ct);

        // Assert
        Assert.True(removed);
        Assert.False(exists);
    }

    [Fact]
    public async Task UpdateAsync_Should_Add_New_Subcategory()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CategoryRepository repository = new(context);

        Guid categoryId = Guid.NewGuid();

        Category category =
            Category.Create(categoryId, "Security");

        await repository.AddAsync(category, ct);
        await context.SaveChangesAsync(ct);

        category.AddSubcategory("Ethical Hacking");

        // Act
        await repository.UpdateAsync(category, ct);
        await context.SaveChangesAsync(ct);

        bool subExists =
            await context.Set<SubcategoryEntity>()
                .AnyAsync(x => x.CategoryId == categoryId, ct);

        // Assert
        Assert.True(subExists);
    }

    [Fact]
    public async Task SubcategoryExistsAsync_Should_Return_True_When_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CategoryRepository repository = new(context);

        Guid categoryId = Guid.NewGuid();

        Category category =
            Category.Create(categoryId, "Cloud");

        category.AddSubcategory("Azure");

        await repository.AddAsync(category, ct);
        await context.SaveChangesAsync(ct);

        await repository.UpdateAsync(category, ct);
        await context.SaveChangesAsync(ct);

        SubcategoryEntity sub =
            await context.Set<SubcategoryEntity>().FirstAsync(ct);

        // Act
        bool result =
            await repository.SubcategoryExistsAsync(sub.Id, ct);

        // Assert
        Assert.True(result);
    }
}
