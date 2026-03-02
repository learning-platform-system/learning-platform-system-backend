using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Application.Students;
using LearningPlatformSystem.Domain.Students;
using Moq;

namespace LearningPlatformSystem.Application.Tests.Students;

public abstract class StudentServiceTestBase
{
    protected readonly Mock<IStudentRepository> StudentRepositoryMock;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly StudentService Service;
    protected readonly CancellationToken CancellationToken;

    protected StudentServiceTestBase()
    {
        StudentRepositoryMock = new Mock<IStudentRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();

        Service = new StudentService(
            StudentRepositoryMock.Object,
            UnitOfWorkMock.Object
        );

        CancellationToken = CancellationToken.None;
    }
}