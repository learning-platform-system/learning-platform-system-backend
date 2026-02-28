using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence.Repositories;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class CourseRepositoryTests(SqliteInMemoryFixture _fixture)
{
    [Fact]
    public async Task AddAsync_Should_Add_Course()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        Guid subcategoryId = await CreateSubcategoryAsync(context, ct, "Programming", "Backend");

        CourseRepository repository = new(context);

        Course course =
            Course.Create(
                subcategoryId,
                "C# Fundamentals",
                "Intro course",
                10);

        // Act
        await repository.AddAsync(course, ct);
        await context.SaveChangesAsync(ct);

        bool exists =
            await context.Courses.AnyAsync(x => x.Id == course.Id, ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_Should_Return_True_When_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        Guid subcategoryId = await CreateSubcategoryAsync(context, ct, "Programming", "Backend");

        CourseRepository repository = new(context);

        Course course =
            Course.Create(subcategoryId, "ASP.NET Core", null, 15);

        await repository.AddAsync(course, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool result = await repository.ExistsAsync(course.Id, ct);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsByTitleAsync_Should_Return_True_When_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        Guid subcategoryId = await CreateSubcategoryAsync(context, ct, "Programming", "Backend");

        CourseRepository repository = new(context);

        Course course =
            Course.Create(subcategoryId, "Entity Framework", null, 10);

        await repository.AddAsync(course, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool result = await repository.ExistsByTitleAsync("Entity Framework", ct);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Course()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        Guid subcategoryId = await CreateSubcategoryAsync(context, ct, "Programming", "Backend");

        CourseRepository repository = new(context);

        Course course =
            Course.Create(subcategoryId, "Clean Architecture", null, 5);

        await repository.AddAsync(course, ct);
        await context.SaveChangesAsync(ct);

        // Act
        Course? result = await repository.GetByIdAsync(course.Id, ct);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Clean Architecture", result!.Title);
    }

    [Fact]
    public async Task RemoveAsync_Should_Remove_Course()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        Guid subcategoryId = await CreateSubcategoryAsync(context, ct, "Programming", "Backend");

        CourseRepository repository = new(context);

        Course course =
            Course.Create(subcategoryId, "Docker Basics", null, 5);

        await repository.AddAsync(course, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool removed = await repository.RemoveAsync(course.Id, ct);
        await context.SaveChangesAsync(ct);

        bool exists =
            await context.Courses.AnyAsync(x => x.Id == course.Id, ct);

        // Assert
        Assert.True(removed);
        Assert.False(exists);
    }

    [Fact]
    public async Task SearchAsync_Should_Filter_By_Title()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        Guid subcategoryId = await CreateSubcategoryAsync(context, ct, "Programming", "Backend");

        CourseRepository repository = new(context);

        Course course1 =
            Course.Create(subcategoryId, "C# Advanced", null, 10);

        Course course2 =
            Course.Create(subcategoryId, "Java Basics", null, 10);

        await repository.AddAsync(course1, ct);
        await repository.AddAsync(course2, ct);
        await context.SaveChangesAsync(ct);

        // Act
        IReadOnlyList<Course> result =
            await repository.SearchAsync("C#", null, ct);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task SearchAsync_Should_Filter_By_Subcategory()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        Guid subcategory1 = await CreateSubcategoryAsync(context, ct, "Programming-1", "Backend-1");
        Guid subcategory2 = await CreateSubcategoryAsync(context, ct, "Programming-2", "Backend-2");

        CourseRepository repository = new(context);

        Course course1 =
            Course.Create(subcategory1, "Azure", null, 10);

        Course course2 =
            Course.Create(subcategory2, "AWS", null, 10);

        await repository.AddAsync(course1, ct);
        await repository.AddAsync(course2, ct);
        await context.SaveChangesAsync(ct);

        // Act
        IReadOnlyList<Course> result =
            await repository.SearchAsync(null, subcategory1, ct);

        // Assert
        Assert.Single(result);
    }



    private static async Task<Guid> CreateSubcategoryAsync(
       LearningPlatformDbContext context,
       CancellationToken ct,
       string categoryName,
       string subcategoryName)
    {
        Guid categoryId = Guid.NewGuid();
        Guid subcategoryId = Guid.NewGuid();

        await context.Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO Categories (Id, Name) VALUES ({categoryId}, {categoryName})",
            ct);

        await context.Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO Subcategories (Id, CategoryId, Name) VALUES ({subcategoryId}, {categoryId}, {subcategoryName})",
            ct);

        return subcategoryId;
    }
}
