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
        Guid campusId = Guid.NewGuid();

        AddCampusContactInformationInput input =
            new AddCampusContactInformationInput(
                Id: campusId,
                Email: "test@test.se",
                PhoneNumber: "123456"
            );

        CampusRepositoryMock
            .Setup(repository => repository.GetByIdAsync(campusId, CancellationToken))
            .ReturnsAsync((Campus?)null);

        // Act
        ApplicationResult result =
            await Service.AddCampusContactInformationAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CampusApplicationErrors.NotFound(campusId).Message,
            result.Error!.Message
        );

        CampusRepositoryMock.Verify(
            repository => repository.UpdateAsync(It.IsAny<Campus>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task AddCampusContactInformationAsync_Should_Update_And_Save_When_Campus_Exists()
    {
        // Arrange
        Guid campusId = Guid.NewGuid();

        Campus campus =
            Campus.Create("Stockholm", "Gatan 1", "12345", "Stockholm");

        AddCampusContactInformationInput input =
            new AddCampusContactInformationInput(
                Id: campusId,
                Email: "mail@test.se",
                PhoneNumber: "0700000000"
            );

        CampusRepositoryMock
            .Setup(repository => repository.GetByIdAsync(campusId, CancellationToken))
            .ReturnsAsync(campus);

        // Act
        ApplicationResult result =
            await Service.AddCampusContactInformationAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        CampusRepositoryMock.Verify(
            repository => repository.UpdateAsync(campus, CancellationToken),
            Times.Once
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task CreateCampusAsync_Should_Return_NameAlreadyExists_When_Name_Exists()
    {
        // Arrange
        string name = "Stockholm";

        CreateCampusInput input =
            new CreateCampusInput(
                Name: name,
                Street: "Gatan 1",
                PostalCode: "12345",
                City: "Stockholm"
            );

        CampusRepositoryMock
            .Setup(repository => repository.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCampusAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CampusApplicationErrors.NameAlreadyExists(name).Message,
            result.Error!.Message
        );

        CampusRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Campus>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task CreateCampusAsync_Should_Add_And_Save_When_Name_Does_Not_Exist()
    {
        // Arrange
        string name = "Göteborg";

        CreateCampusInput input =
            new CreateCampusInput(
                Name: name,
                Street: "Gatan 2",
                PostalCode: "22222",
                City: "Göteborg"
            );

        CampusRepositoryMock
            .Setup(repository => repository.ExistsByNameAsync(name, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCampusAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);

        CampusRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Campus>(), CancellationToken),
            Times.Once
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteCampusAsync_Should_Return_NotFound_When_Campus_Does_Not_Exist()
    {
        // Arrange
        Guid campusId = Guid.NewGuid();

        CampusRepositoryMock
            .Setup(repository => repository.RemoveAsync(campusId, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.DeleteCampusAsync(campusId, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CampusApplicationErrors.NotFound(campusId).Message,
            result.Error!.Message
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteCampusAsync_Should_Remove_And_Save_When_Campus_Exists()
    {
        // Arrange
        Guid campusId = Guid.NewGuid();

        CampusRepositoryMock
            .Setup(repository => repository.RemoveAsync(campusId, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult result =
            await Service.DeleteCampusAsync(campusId, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Once
        );
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
            .Setup(repository => repository.GetAllAsync(CancellationToken))
            .ReturnsAsync(campuses);

        // Act
        ApplicationResult<IReadOnlyList<CampusOutput>> result =
            await Service.GetAllCampusesAsync(CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
        Assert.Equal("Malmö", result.Data![0].Name);
        Assert.Equal("Testgatan 3", result.Data![0].Street);
        Assert.Equal("33333", result.Data![0].PostalCode);
        Assert.Equal("Malmö", result.Data![0].City);
    }
}
