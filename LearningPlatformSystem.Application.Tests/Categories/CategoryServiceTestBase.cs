using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Categories;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Categories;

public abstract class CategoryServiceTestBase
{
    protected readonly Mock<ICategoryRepository> CategoryRepositoryMock;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly CategoryService Service;
    protected readonly CancellationToken CancellationToken;

    protected CategoryServiceTestBase()
    {
        CategoryRepositoryMock = new Mock<ICategoryRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();

        Service = new CategoryService(
            CategoryRepositoryMock.Object,
            UnitOfWorkMock.Object
        );

        CancellationToken = CancellationToken.None;
    }
}
