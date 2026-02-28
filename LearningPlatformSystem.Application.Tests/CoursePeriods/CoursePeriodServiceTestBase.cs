using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Domain.Teachers;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace LearningPlatformSystem.Application.Tests.CoursePeriods;

public abstract class CoursePeriodServiceTestBase
{
    protected readonly Mock<ICoursePeriodRepository> CoursePeriodRepositoryMock;
    protected readonly Mock<ICourseRepository> CourseRepositoryMock;
    protected readonly Mock<ITeacherRepository> TeacherRepositoryMock;
    protected readonly Mock<ICampusRepository> CampusRepositoryMock;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly IMemoryCache MemoryCache;
    protected readonly CoursePeriodService Service;
    protected readonly CancellationToken CancellationToken;

    protected CoursePeriodServiceTestBase()
    {
        CoursePeriodRepositoryMock = new Mock<ICoursePeriodRepository>();
        CourseRepositoryMock = new Mock<ICourseRepository>();
        TeacherRepositoryMock = new Mock<ITeacherRepository>();
        CampusRepositoryMock = new Mock<ICampusRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();

        MemoryCache = new MemoryCache(new MemoryCacheOptions());

        Service = new CoursePeriodService(
            CoursePeriodRepositoryMock.Object,
            CourseRepositoryMock.Object,
            TeacherRepositoryMock.Object,
            CampusRepositoryMock.Object,
            UnitOfWorkMock.Object,
            MemoryCache
        );

        CancellationToken = CancellationToken.None;
    }
}