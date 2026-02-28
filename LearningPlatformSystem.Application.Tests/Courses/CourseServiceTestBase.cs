using LearningPlatformSystem.Application.Courses;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Courses;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Courses;

public abstract class CourseServiceTestBase
{
    protected readonly Mock<ICourseRepository> CourseRepositoryMock;
    protected readonly Mock<ICategoryRepository> CategoryRepositoryMock;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly CourseService Service;
    protected readonly CancellationToken CancellationToken;

    protected CourseServiceTestBase()
    {
        CourseRepositoryMock = new Mock<ICourseRepository>();
        CategoryRepositoryMock = new Mock<ICategoryRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();

        Service = new CourseService(
            CourseRepositoryMock.Object,
            UnitOfWorkMock.Object,
            CategoryRepositoryMock.Object
        );

        CancellationToken = CancellationToken.None;
    }
}
