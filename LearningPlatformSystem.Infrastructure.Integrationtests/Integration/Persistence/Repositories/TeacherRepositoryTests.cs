using LearningPlatformSystem.Domain.Teachers;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence.Repositories;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class TeacherRepositoryTests(SqliteInMemoryFixture _fixture)
{
    [Fact]
    public async Task AddAsync_Should_Add_Teacher()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        TeacherRepository repository = new(context);

        Teacher teacher =
            Teacher.Create(
                "Anna",
                "Andersson",
                "anna.teacher@test.se",
                "0701111111");

        // Act
        await repository.AddAsync(teacher, ct);
        await context.SaveChangesAsync(ct);

        bool exists =
            await context.Teachers.AnyAsync(x => x.Id == teacher.Id, ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_Should_Return_True_When_Teacher_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        TeacherRepository repository = new(context);

        Teacher teacher =
            Teacher.Create(
                "Erik",
                "Eriksson",
                "erik.teacher@test.se",
                "0702222222");

        await repository.AddAsync(teacher, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool exists =
            await repository.ExistsAsync(teacher.Id, ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsWithTheSameEmailAsync_Should_Return_True_When_Email_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        TeacherRepository repository = new(context);

        Teacher teacher =
            Teacher.Create(
                "Lisa",
                "Larsson",
                "lisa.teacher@test.se",
                "0703333333");

        await repository.AddAsync(teacher, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool exists =
            await repository.ExistsWithTheSameEmailAsync("lisa.teacher@test.se", ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Teacher_When_Found()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        TeacherRepository repository = new(context);

        Teacher teacher =
            Teacher.Create(
                "Kalle",
                "Karlsson",
                "kalle.teacher@test.se",
                "0704444444");

        await repository.AddAsync(teacher, ct);
        await context.SaveChangesAsync(ct);

        // Act
        Teacher? result =
            await repository.GetByIdAsync(teacher.Id, ct);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Kalle", result!.Name.FirstName);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Teachers()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        TeacherRepository repository = new(context);

        Teacher teacher1 =
            Teacher.Create("A", "A", "a.teacher@test.se", "07077771");

        Teacher teacher2 =
            Teacher.Create("B", "B", "b.teacher@test.se", "079999902");

        await repository.AddAsync(teacher1, ct);
        await repository.AddAsync(teacher2, ct);
        await context.SaveChangesAsync(ct);

        // Act
        IReadOnlyList<Teacher> teachers =
            await repository.GetAllAsync(ct);

        // Assert
        Assert.Equal(2, teachers.Count);
    }

    [Fact]
    public async Task RemoveAsync_Should_Remove_Teacher_When_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        TeacherRepository repository = new(context);

        Teacher teacher =
            Teacher.Create(
                "Sara",
                "Svensson",
                "sara.teacher@test.se",
                "0705555555");

        await repository.AddAsync(teacher, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool removed =
            await repository.RemoveAsync(teacher.Id, ct);

        await context.SaveChangesAsync(ct);

        bool exists =
            await context.Teachers.AnyAsync(x => x.Id == teacher.Id, ct);

        // Assert
        Assert.True(removed);
        Assert.False(exists);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Address()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        TeacherRepository repository = new(context);

        Teacher teacher =
            Teacher.Create(
                "Olle",
                "Olsson",
                "olle.teacher@test.se",
                "0706666666");

        teacher.AddAddress("Old Street", "11111", "Stockholm");

        await repository.AddAsync(teacher, ct);
        await context.SaveChangesAsync(ct);

        teacher.ChangeAddress("New Street", "22222", "Göteborg");

        // Act
        await repository.UpdateAsync(teacher, ct);
        await context.SaveChangesAsync(ct);

        var entity =
            await context.Teachers.SingleAsync(x => x.Id == teacher.Id, ct);

        // Assert
        Assert.Equal("New Street", entity.Address!.Street);
    }
}