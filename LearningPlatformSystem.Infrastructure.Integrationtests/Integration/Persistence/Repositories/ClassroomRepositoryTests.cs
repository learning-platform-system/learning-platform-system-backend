using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;

namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence.Repositories;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class ClassroomRepositoryTests(SqliteInMemoryFixture _fixture)
{
    [Fact]
    public async Task AddAsync_Should_Persist_Classroom()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        await using LearningPlatformDbContext context =
            _fixture.CreateContext();

        ClassroomRepository repository =
            new ClassroomRepository(context);

        Classroom classroom =
            Classroom.Create(
                Guid.NewGuid(),
                "B202",
                30,
                ClassroomType.LectureHall);

        // Act
        await repository.AddAsync(classroom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        Classroom? result =
            await repository.GetByIdAsync(classroom.Id, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("B202", result!.Name);
        Assert.Equal(30, result.Capacity);
    }

    [Fact]
    public async Task GetByTypeAsync_Should_Return_Only_Matching_Type()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        await using LearningPlatformDbContext context =
            _fixture.CreateContext();

        ClassroomRepository repository =
            new ClassroomRepository(context);

        Classroom lectureHall =
            Classroom.Create(Guid.NewGuid(), "A101", 30, ClassroomType.LectureHall);

        Classroom labRoom =
            Classroom.Create(Guid.NewGuid(), "B302", 20, ClassroomType.Laboratory);

        await repository.AddAsync(lectureHall, cancellationToken);
        await repository.AddAsync(labRoom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // Act
        IReadOnlyList<Classroom> result =
            await repository.GetByTypeAsync(ClassroomType.LectureHall, cancellationToken);

        // Assert
        Assert.Single(result);
        Assert.Equal(ClassroomType.LectureHall, result[0].Type);
    }

    [Fact]
    public async Task RemoveAsync_Should_Remove_When_Exists()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        await using LearningPlatformDbContext context =
            _fixture.CreateContext();

        ClassroomRepository repository =
            new ClassroomRepository(context);

        Classroom classroom =
            Classroom.Create(Guid.NewGuid(), "A15", 25, ClassroomType.LectureHall);

        await repository.AddAsync(classroom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // Act
        bool removed =
            await repository.RemoveAsync(classroom.Id, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        Classroom? result =
            await repository.GetByIdAsync(classroom.Id, cancellationToken);

        // Assert
        Assert.True(removed);
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveAsync_Should_ReturnFalse_When_NotExists()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        await using LearningPlatformDbContext context =
            _fixture.CreateContext();

        ClassroomRepository repository =
            new ClassroomRepository(context);

        Guid nonExistingId = Guid.NewGuid();

        // Act
        bool removed =
            await repository.RemoveAsync(nonExistingId, cancellationToken);

        // Assert
        Assert.False(removed);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Values()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        await using LearningPlatformDbContext context =
            _fixture.CreateContext();

        ClassroomRepository repository =
            new ClassroomRepository(context);

        Classroom classroom =
            Classroom.Create(Guid.NewGuid(), "A301", 20, ClassroomType.LectureHall);

        await repository.AddAsync(classroom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // Act
        Classroom updated =
            Classroom.Create(classroom.Id, "B402", 50, ClassroomType.Laboratory);

        await repository.UpdateAsync(updated, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        Classroom? result =
            await repository.GetByIdAsync(classroom.Id, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("B402", result!.Name);
        Assert.Equal(50, result.Capacity);
        Assert.Equal(ClassroomType.Laboratory, result.Type);
    }

    [Fact]
    public async Task ExistsByNameAsync_Should_ReturnTrue_When_Exists()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        await using LearningPlatformDbContext context =
            _fixture.CreateContext();

        ClassroomRepository repository =
            new ClassroomRepository(context);

        string classroomName = "C1001";

        Classroom classroom =
            Classroom.Create(Guid.NewGuid(), classroomName, 25, ClassroomType.LectureHall);

        await repository.AddAsync(classroom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // Act
        bool exists =
            await repository.ExistsByNameAsync(classroomName, cancellationToken);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAnotherWithSameNameAsync_Should_ReturnTrue_When_NameExists_ForAnotherId()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        await using LearningPlatformDbContext context =
            _fixture.CreateContext();

        ClassroomRepository repository =
            new ClassroomRepository(context);

        string name = "G1001";

        Classroom classroom =
            Classroom.Create(Guid.NewGuid(), name, 20, ClassroomType.LectureHall);

        await repository.AddAsync(classroom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        Guid differentId = Guid.NewGuid();

        // Act
        bool existsAnother =
            await repository.ExistsAnotherWithSameNameAsync(name, differentId, cancellationToken);

        // Assert
        Assert.True(existsAnother);
    }
}