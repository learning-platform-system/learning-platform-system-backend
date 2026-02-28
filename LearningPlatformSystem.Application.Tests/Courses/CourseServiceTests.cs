using LearningPlatformSystem.Application.Courses;
using LearningPlatformSystem.Application.Courses.Inputs;
using LearningPlatformSystem.Application.Courses.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Courses;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Courses;

public class CourseServiceTests : CourseServiceTestBase
{
    [Fact]
    public async Task CreateCourseAsync_Should_Return_TitleAlreadyExists_When_Title_Exists()
    {
        // Arrange
        string title = "C# Advanced";

        CreateCourseInput input = new CreateCourseInput
        (
            SubcategoryId: Guid.NewGuid(),
            Title: title,
            Description: "Test description",
            Credits: 5
        );

        CourseRepositoryMock
            .Setup(repository => repository.ExistsByTitleAsync(title, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCourseAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CourseApplicationErrors.TitleAlreadyExists(title).Message,
            result.Error!.Message
        );

        CourseRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Course>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task CreateCourseAsync_Should_Return_SubcategoryNotFound_When_Subcategory_Does_Not_Exist()
    {
        // Arrange
        Guid subcategoryId = Guid.NewGuid();

        CreateCourseInput input = new CreateCourseInput
        (
            SubcategoryId: subcategoryId,
            Title: "C#",
            Description: "Test",
            Credits: 5
        );

        CourseRepositoryMock
            .Setup(repository => repository.ExistsByTitleAsync(input.Title, CancellationToken))
            .ReturnsAsync(false);

        CategoryRepositoryMock
            .Setup(repository => repository.SubcategoryExistsAsync(subcategoryId, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCourseAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CourseApplicationErrors.SubcategoryNotFound(subcategoryId).Message,
            result.Error!.Message
        );

        CourseRepositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Course>(), CancellationToken),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteCourseAsync_Should_Return_NotFound_When_Course_Does_Not_Exist()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();

        CourseRepositoryMock
            .Setup(repository => repository.RemoveAsync(courseId, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult result =
            await Service.DeleteCourseAsync(courseId, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CourseApplicationErrors.NotFound(courseId).Message,
            result.Error!.Message
        );

        UnitOfWorkMock.Verify(
            unit => unit.SaveChangesAsync(CancellationToken),
            Times.Never
        );
    }

    [Fact]
    public async Task GetCourseById_Should_Return_NotFound_When_Course_Does_Not_Exist()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();

        CourseRepositoryMock
            .Setup(repository => repository.GetByIdAsync(courseId, CancellationToken))
            .ReturnsAsync((Course?)null);

        // Act
        ApplicationResult<CourseOutput> result =
            await Service.GetCourseById(courseId, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CourseApplicationErrors.NotFound(courseId).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task SearchCoursesAsync_Should_Return_List_Of_CourseOutputs()
    {
        // Arrange
        Guid subcategoryId = Guid.NewGuid();

        Course course = Course.Create(
            subcategoryId,
            "React",
            "Frontend course",
            7
        );

        IReadOnlyList<Course> courses = new List<Course> { course };

        SearchCoursesInput input = new SearchCoursesInput
        { 
            Title = "React",
            SubcategoryId = subcategoryId
        };

        CourseRepositoryMock
            .Setup(repository => repository.SearchAsync(input.Title, input.SubcategoryId, CancellationToken))
            .ReturnsAsync(courses);

        // Act
        ApplicationResult<IReadOnlyList<CourseOutput>> result =
            await Service.SearchCoursesAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
        Assert.Equal(course.Title, result.Data![0].Title);
    }
}