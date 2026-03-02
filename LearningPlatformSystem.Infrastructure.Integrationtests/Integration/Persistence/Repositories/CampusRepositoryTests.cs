//using LearningPlatformSystem.Domain.Campuses;
//using LearningPlatformSystem.Infrastructure.Persistence.EFC;
//using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
//using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
//using Microsoft.EntityFrameworkCore;

//namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence.Repositories;

//[Collection(SqliteInMemoryCollection.Name)]
//public sealed class CampusRepositoryTests(SqliteInMemoryFixture _fixture)
//{
//    [Fact]
//    public async Task AddAsync_Should_Add_Campus()
//    {
//        // Arrange
//        CancellationToken ct = CancellationToken.None;

//        await _fixture.ClearDatabaseAsync();
//        await using LearningPlatformDbContext context = _fixture.CreateContext();

//        CampusRepository repository = new(context);

//        Campus campus =
//            Campus.Create(
//                "Stockholm Campus",
//                "Main Street 1",
//                "11111",
//                "Stockholm"
//            );

//        // Act
//        await repository.AddAsync(campus, ct);
//        await context.SaveChangesAsync(ct);

//        CampusEntity? entity =
//            await context.Campuses.SingleOrDefaultAsync(x => x.Id == campus.Id, ct);

//        // Assert
//        Assert.NotNull(entity);
//        Assert.Equal("Stockholm Campus", entity!.Name);
//    }

//    [Fact]
//    public async Task ExistsAsync_Should_Return_True_When_Exists()
//    {
//        // Arrange
//        CancellationToken ct = CancellationToken.None;

//        await _fixture.ClearDatabaseAsync();
//        await using LearningPlatformDbContext context = _fixture.CreateContext();

//        Campus campus =
//            Campus.Create(
//                "Göteborg Campus",
//                "Street 1",
//                "22222",
//                "Göteborg"
//            );

//        CampusRepository repository = new(context);

//        await repository.AddAsync(campus, ct);
//        await context.SaveChangesAsync(ct);

//        // Act
//        bool result = await repository.ExistsAsync(campus.Id, ct);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public async Task ExistsByNameAsync_Should_Return_True_When_Name_Exists()
//    {
//        // Arrange
//        CancellationToken ct = CancellationToken.None;

//        await _fixture.ClearDatabaseAsync();
//        await using LearningPlatformDbContext context = _fixture.CreateContext();

//        Campus campus =
//            Campus.Create(
//                "Malmö Campus",
//                "Street 2",
//                "33333",
//                "Malmö"
//            );

//        CampusRepository repository = new(context);

//        await repository.AddAsync(campus, ct);
//        await context.SaveChangesAsync(ct);

//        // Act
//        bool result = await repository.ExistsByNameAsync("Malmö Campus", ct);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public async Task GetAllAsync_Should_Return_All_Campuses()
//    {
//        // Arrange
//        CancellationToken ct = CancellationToken.None;

//        await _fixture.ClearDatabaseAsync();
//        await using LearningPlatformDbContext context = _fixture.CreateContext();

//        CampusRepository repository = new(context);

//        Campus campus1 =
//            Campus.Create(
//                "Campus 1",
//                "Street 1",
//                "44444",
//                "City 1"
//            );

//        Campus campus2 =
//            Campus.Create(
//                "Campus 2",
//                "Street 2",
//                "55555",
//                "City 2"
//            );

//        await repository.AddAsync(campus1, ct);
//        await repository.AddAsync(campus2, ct);
//        await context.SaveChangesAsync(ct);

//        // Act
//        IReadOnlyList<Campus> result = await repository.GetAllAsync(ct);

//        // Assert
//        Assert.Equal(2, result.Count);
//    }

//    [Fact]
//    public async Task GetByIdAsync_Should_Return_Campus_When_Found()
//    {
//        // Arrange
//        CancellationToken ct = CancellationToken.None;

//        await _fixture.ClearDatabaseAsync();
//        await using LearningPlatformDbContext context = _fixture.CreateContext();

//        CampusRepository repository = new(context);

//        Campus campus =
//            Campus.Create(
//                "Uppsala Campus",
//                "Street 3",
//                "66666",
//                "Uppsala"
//            );

//        await repository.AddAsync(campus, ct);
//        await context.SaveChangesAsync(ct);

//        // Act
//        Campus? result = await repository.GetByIdAsync(campus.Id, ct);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("Uppsala Campus", result!.Name);
//    }

//    [Fact]
//    public async Task RemoveAsync_Should_Remove_Campus_When_Exists()
//    {
//        // Arrange
//        CancellationToken ct = CancellationToken.None;

//        await _fixture.ClearDatabaseAsync();
//        await using LearningPlatformDbContext context = _fixture.CreateContext();

//        CampusRepository repository = new(context);

//        Campus campus =
//            Campus.Create(
//                "Västerås Campus",
//                "Street 4",
//                "77777",
//                "Västerås"
//            );

//        await repository.AddAsync(campus, ct);
//        await context.SaveChangesAsync(ct);

//        // Act
//        bool removed = await repository.RemoveAsync(campus.Id, ct);
//        await context.SaveChangesAsync(ct);

//        bool existsAfter =
//            await context.Campuses.AnyAsync(x => x.Id == campus.Id, ct);

//        // Assert
//        Assert.True(removed);
//        Assert.False(existsAfter);
//    }

//    [Fact]
//    public async Task UpdateAsync_Should_Update_ContactInformation()
//    {
//        // Arrange
//        CancellationToken ct = CancellationToken.None;

//        await _fixture.ClearDatabaseAsync();
//        await using LearningPlatformDbContext context = _fixture.CreateContext();

//        CampusRepository repository = new(context);

//        Campus campus =
//            Campus.Create(
//                "Örebro Campus",
//                "Street 5",
//                "88888",
//                "Örebro"
//            );

//        await repository.AddAsync(campus, ct);
//        await context.SaveChangesAsync(ct);

//        campus.AddContactInformation("info@orebro.se", "0701234567");

//        // Act
//        await repository.UpdateAsync(campus, ct);
//        await context.SaveChangesAsync(ct);

//        CampusEntity updated =
//            await context.Campuses.SingleAsync(x => x.Id == campus.Id, ct);

//        // Assert
//        Assert.NotNull(updated.ContactInformation);
//    }
//}
