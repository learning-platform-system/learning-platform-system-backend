using LearningPlatformSystem.Domain.Students;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence.Repositories;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class StudentRepositoryTests(SqliteInMemoryFixture _fixture)
{
    [Fact]
    public async Task AddAsync_Should_Add_Student()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        StudentRepository repository = new(context);

        Student student =
            Student.Create(
                "Anna",
                "Andersson",
                "anna@test.se",
                "0701234567");

        // Act
        await repository.AddAsync(student, ct);
        await context.SaveChangesAsync(ct);

        bool exists =
            await context.Students.AnyAsync(x => x.Id == student.Id, ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_Should_Return_True_When_Student_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        StudentRepository repository = new(context);

        Student student =
            Student.Create(
                "Erik",
                "Eriksson",
                "erik@test.se",
                "0700000000");

        await repository.AddAsync(student, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool exists =
            await repository.ExistsAsync(student.Id, ct);

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

        StudentRepository repository = new(context);

        Student student =
            Student.Create(
                "Lisa",
                "Larsson",
                "lisa@test.se",
                "0709999999");

        await repository.AddAsync(student, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool exists =
            await repository.ExistsWithTheSameEmailAsync("lisa@test.se", ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Student_When_Found()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        StudentRepository repository = new(context);

        Student student =
            Student.Create(
                "Kalle",
                "Karlsson",
                "kalle@test.se",
                "0701111111");

        await repository.AddAsync(student, ct);
        await context.SaveChangesAsync(ct);

        // Act
        Student? result =
            await repository.GetByIdAsync(student.Id, ct);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Kalle", result!.Name.FirstName);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Students()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        StudentRepository repository = new(context);

        Student student1 =
            Student.Create("A", "A", "a@test.se", "0701563786");

        Student student2 =
            Student.Create("B", "B", "b@test.se", "07077772");

        await repository.AddAsync(student1, ct);
        await repository.AddAsync(student2, ct);
        await context.SaveChangesAsync(ct);

        // Act
        IReadOnlyList<Student> students =
            await repository.GetAllAsync(ct);

        // Assert
        Assert.Equal(2, students.Count);
    }

    [Fact]
    public async Task RemoveAsync_Should_Remove_Student_When_Exists()
    {
        // Arrange
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        StudentRepository repository = new(context);

        Student student =
            Student.Create(
                "Sara",
                "Svensson",
                "sara@test.se",
                "0703333333");

        await repository.AddAsync(student, ct);
        await context.SaveChangesAsync(ct);

        // Act
        bool removed =
            await repository.RemoveAsync(student.Id, ct);

        await context.SaveChangesAsync(ct);

        bool exists =
            await context.Students.AnyAsync(x => x.Id == student.Id, ct);

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

        StudentRepository repository = new(context);

        Student student =
            Student.Create(
                "Olle",
                "Olsson",
                "olle@test.se",
                "0704444444");

        student.AddAddress("Old Street", "11111", "Stockholm");

        await repository.AddAsync(student, ct);
        await context.SaveChangesAsync(ct);

        student.ChangeAddress("New Street", "22222", "Göteborg");

        // Act
        await repository.UpdateAsync(student, ct);
        await context.SaveChangesAsync(ct);

        var entity =
            await context.Students.SingleAsync(x => x.Id == student.Id, ct);

        // Assert
        Assert.Equal("New Street", entity.Address!.Street);
    }
}
