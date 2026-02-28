using LearningPlatformSystem.Application.Campuses;
using LearningPlatformSystem.Application.Campuses.Inputs;
using LearningPlatformSystem.Application.Campuses.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Campuses;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Campuses;

public class CampusServiceTests : CampusServiceTestBase
{
    [Fact]
    public async Task AddCampusContactInformationAsync_Should_Return_NotFound_When_Campus_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        AddCampusContactInformationInput input =
            new AddCampusContactInformationInput(id, "mail@test.se", "0700000000");

        CampusRepositoryMock
            .Setup(r => r.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync((Campus?)null);

        // Act
        ApplicationResult result =
            await Service.AddCampusContactInformationAsync(input, CancellationToken);

        // Assert
        ApplicationResultError expected =
            CampusApplicationErrors.NotFound(id);

        Assert.False(result.IsSuccess);
        Assert.Equal(expected.Type, result.Error!.Type);
        Assert.Equal(expected.Message, result.Error!.Message);

        CampusRepositoryMock.Verify(
            r => r.UpdateAsync(It.IsAny<Campus>(), CancellationToken),
            Times.Never);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Never);
    }

    [Fact]
    public async Task AddCampusContactInformationAsync_Should_Update_And_Save_When_Campus_Exists()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        Campus campus =
            Campus.Create("Stockholm", "Gatan 1", "11111", "Stockholm");

        AddCampusContactInformationInput input =
            new AddCampusContactInformationInput(id, "mail@test.se", "0700000000");

        CampusRepositoryMock
            .Setup(r => r.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync(campus);

        // Act
        ApplicationResult result =
            await Service.AddCampusContactInformationAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        CampusRepositoryMock.Verify(
            r => r.UpdateAsync(campus, CancellationToken),
            Times.Once);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task CreateCampusAsync_Should_Return_NameAlreadyExists_When_Name_Exists()
    {
        // Arrange
        string name = "Stockholm";

        CreateCampusInput input =
            new CreateCampusInput(name, "Gatan 1", "11111", "Stockholm");

        CampusRepositoryMock
            .Setup(r => r.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCampusAsync(input, CancellationToken);

        // Assert
        ApplicationResultError expected =
            CampusApplicationErrors.NameAlreadyExists(name);

        Assert.False(result.IsSuccess);
        Assert.Equal(expected.Type, result.Error!.Type);
        Assert.Equal(expected.Message, result.Error!.Message);

        CampusRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Campus>(), CancellationToken),
            Times.Never);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Never);
    }

    [Fact]
    public async Task CreateCampusAsync_Should_Add_And_Save_When_Name_Does_Not_Exist()
    {
        // Arrange
        string name = "Göteborg";

        CreateCampusInput input =
            new CreateCampusInput(name, "Gatan 2", "22222", "Göteborg");

        CampusRepositoryMock
            .Setup(r => r.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCampusAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);

        CampusRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Campus>(), CancellationToken),
            Times.Once);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task DeleteCampusAsync_Should_Return_NotFound_When_Campus_Does_Not_Exist()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        CampusRepositoryMock
            .Setup(r => r.RemoveAsync(id, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.DeleteCampusAsync(id, CancellationToken);

        // Assert
        ApplicationResultError expected =
            CampusApplicationErrors.NotFound(id);

        Assert.False(result.IsSuccess);
        Assert.Equal(expected.Type, result.Error!.Type);
        Assert.Equal(expected.Message, result.Error!.Message);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Never);
    }

    [Fact]
    public async Task DeleteCampusAsync_Should_Remove_And_Save_When_Campus_Exists()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        CampusRepositoryMock
            .Setup(r => r.RemoveAsync(id, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult result =
            await Service.DeleteCampusAsync(id, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task GetAllCampusesAsync_Should_Return_Mapped_CampusOutputs()
    {
        // Arrange
        Campus campus =
            Campus.Create("Malmö", "Testgatan 3", "33333", "Malmö");

        IReadOnlyList<Campus> campuses =
            new List<Campus> { campus };

        CampusRepositoryMock
            .Setup(r => r.GetAllAsync(CancellationToken))
            .ReturnsAsync(campuses);

        // Act
        ApplicationResult<IReadOnlyList<CampusOutput>> result =
            await Service.GetAllCampusesAsync(CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
        Assert.Equal("Malmö", result.Data![0].Name);
        Assert.Equal("Testgatan 3", result.Data[0].Street);
        Assert.Equal("33333", result.Data[0].PostalCode);
        Assert.Equal("Malmö", result.Data[0].City);
    }
}