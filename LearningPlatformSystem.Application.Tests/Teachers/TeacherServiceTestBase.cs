using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Teachers;
using LearningPlatformSystem.Domain.Teachers;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Teachers;

public abstract class TeacherServiceTestBase
{
    protected readonly Mock<ITeacherRepository> TeacherRepositoryMock;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly TeacherService Service;
    protected readonly CancellationToken CancellationToken;

    protected TeacherServiceTestBase()
    {
        TeacherRepositoryMock = new Mock<ITeacherRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();

        Service = new TeacherService(
            TeacherRepositoryMock.Object,
            UnitOfWorkMock.Object
        );

        CancellationToken = CancellationToken.None;
    }
}
