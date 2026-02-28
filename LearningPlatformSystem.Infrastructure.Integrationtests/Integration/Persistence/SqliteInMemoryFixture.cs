using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence;

public sealed class SqliteInMemoryFixture : IAsyncLifetime
{
    private SqliteConnection? _sqliteConnection;
    
    public DbContextOptions<LearningPlatformDbContext> Options { get; private set; } = default!;

    /*
    - Skapar SQLite in-memory
    - Öppnar anslutningen
    - Skapar tabeller
    - Sparar DbContextOptions
    */
    public async Task InitializeAsync()
    {
        _sqliteConnection =
            new SqliteConnection("Data Source=:memory:;Cache=Shared");

        await _sqliteConnection.OpenAsync();

        Options =
            new DbContextOptionsBuilder<LearningPlatformDbContext>()
                .UseSqlite(_sqliteConnection)
                .EnableSensitiveDataLogging()
                .Options;

        await using LearningPlatformDbContext context =
            new LearningPlatformDbContext(Options);

        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        if (_sqliteConnection is not null)
            await _sqliteConnection.DisposeAsync();
    }

    
    // Ger varje test en ny DbContext
    public LearningPlatformDbContext CreateContext()
    {
        return new LearningPlatformDbContext(Options);
    }
}

// Stänger databasen efter alla tester
[CollectionDefinition(Name)]
public sealed class SqliteInMemoryCollection
    : ICollectionFixture<SqliteInMemoryFixture>
{
    public const string Name = "SqliteInMemory";
}
