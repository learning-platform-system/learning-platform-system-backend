using LearningPlatformSystem.Application.Campuses;
using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Application.Shared;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Campuses
{
    public abstract class CampusServiceTestBase
    {
        protected readonly Mock<ICampusRepository> CampusRepositoryMock;
        protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
        protected readonly CampusService Service;
        protected readonly CancellationToken CancellationToken;

        protected CampusServiceTestBase()
        {
            CampusRepositoryMock = new Mock<ICampusRepository>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();

            Service = new CampusService(
                CampusRepositoryMock.Object,
                UnitOfWorkMock.Object
            );

            CancellationToken = CancellationToken.None;
        }
    }
}