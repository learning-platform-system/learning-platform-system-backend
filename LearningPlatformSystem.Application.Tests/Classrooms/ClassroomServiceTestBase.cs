using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Classrooms;

public abstract class ClassroomServiceTestBase
{
    protected readonly Mock<IClassroomRepository> ClassroomRepositoryMock;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly ClassroomService Service;
    protected readonly CancellationToken CancellationToken;

    protected ClassroomServiceTestBase()
    {
        ClassroomRepositoryMock = new Mock<IClassroomRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();

        Service = new ClassroomService(
            ClassroomRepositoryMock.Object,
            UnitOfWorkMock.Object
        );

        CancellationToken = CancellationToken.None;
    }
}