using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.Application.Tests.Shared;

public class ApplicationResultTests
{
    [Fact]
    public void Success_Should_Set_IsSuccess_True()
    {
        // Arrange

        // Act
        ApplicationResult result = ApplicationResult.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Fail_Should_Set_Error()
    {
        // Arrange
        ApplicationResultError error =
            new ApplicationResultError(ErrorTypes.NotFound, "Not found");

        // Act
        ApplicationResult result = ApplicationResult.Fail(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Generic_Success_Should_Set_Data()
    {
        // Arrange
        int value = 10;

        // Act
        ApplicationResult<int> result =
            ApplicationResult<int>.Success(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(10, result.Data);
        Assert.Null(result.Error);
    }
}
