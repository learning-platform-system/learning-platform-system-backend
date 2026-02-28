using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.CoursePeriods.Inputs;
using LearningPlatformSystem.Application.CoursePeriods.Outputs;
using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.CourseSessionAttendances;
using LearningPlatformSystem.Domain.Shared.Enums;
using Moq;

namespace LearningPlatformSystem.Application.Tests.CoursePeriods;

public class CoursePeriodServiceTests : CoursePeriodServiceTestBase
{
    [Fact]
    public async Task GetCoursePeriodByCourseIdAsync_Should_Return_From_Database_When_Not_In_Cache()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();

        CoursePeriod coursePeriod =
            CoursePeriod.Create(
                Guid.NewGuid(),
                courseId,
                Guid.NewGuid(),
                DateOnly.FromDateTime(DateTime.Today),
                DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
                CourseFormat.Online
            );

        IReadOnlyList<CoursePeriod> list = new List<CoursePeriod> { coursePeriod };

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByCourseIdAsync(courseId, CancellationToken))
            .ReturnsAsync(list);

        // Act
        ApplicationResult<IReadOnlyList<CoursePeriodOutput>> result =
            await Service.GetCoursePeriodByCourseIdAsync(courseId, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
    }

    [Fact]
    public async Task AddEnrollmentAsync_Should_Return_NotFound_When_CoursePeriod_Does_Not_Exist()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();

        AddCoursePeriodEnrollmentInput input =
            new AddCoursePeriodEnrollmentInput(studentId, coursePeriodId);

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdWithEnrollmentsAsync(coursePeriodId, CancellationToken))
            .ReturnsAsync((CoursePeriod?)null);

        // Act
        ApplicationResult result =
            await Service.AddEnrollmentAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorTypes.NotFound, result.Error!.Type);
        Assert.Contains(coursePeriodId.ToString(), result.Error!.Message);
    }

    [Fact]
    public async Task CreateCoursePeriodAsync_Should_Return_CourseNotFound()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();
        Guid teacherId = Guid.NewGuid();

        CreateCoursePeriodInput input =
            new CreateCoursePeriodInput(
                courseId,
                teacherId,
                CampusId: null,
                DateOnly.FromDateTime(DateTime.Today),
                DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                CourseFormat.Online
            );

        CourseRepositoryMock
            .Setup(r => r.ExistsAsync(courseId, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCoursePeriodAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CoursePeriodApplicationErrors.CourseNotFound(courseId).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task CreateCoursePeriodAsync_Should_Return_TeacherNotFound()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();
        Guid teacherId = Guid.NewGuid();

        CreateCoursePeriodInput input =
            new CreateCoursePeriodInput(
                courseId,
                teacherId,
                CampusId: null,
                DateOnly.FromDateTime(DateTime.Today),
                DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                CourseFormat.Online
            );

        CourseRepositoryMock
            .Setup(r => r.ExistsAsync(courseId, CancellationToken))
            .ReturnsAsync(true);

        TeacherRepositoryMock
            .Setup(r => r.ExistsAsync(teacherId, CancellationToken))
            .ReturnsAsync(false);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCoursePeriodAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CoursePeriodApplicationErrors.TeacherNotFound(teacherId).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task CreateCoursePeriodAsync_Should_Return_CampusIdIsRequired_When_Onsite_And_Null()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();
        Guid teacherId = Guid.NewGuid();

        CreateCoursePeriodInput input =
            new CreateCoursePeriodInput(
                courseId,
                teacherId,
                CampusId: null,
                DateOnly.FromDateTime(DateTime.Today),
                DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                CourseFormat.Onsite
            );

        CourseRepositoryMock
            .Setup(r => r.ExistsAsync(courseId, CancellationToken))
            .ReturnsAsync(true);

        TeacherRepositoryMock
            .Setup(r => r.ExistsAsync(teacherId, CancellationToken))
            .ReturnsAsync(true);

        // Act
        ApplicationResult<Guid> result =
            await Service.CreateCoursePeriodAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CoursePeriodApplicationErrors.CampusIdIsRequired(null).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task DeleteCoursePeriodAsync_Should_Return_NotFound_When_Not_Exists()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdAsync(id, CancellationToken))
            .ReturnsAsync((CoursePeriod?)null);

        // Act
        ApplicationResult result =
            await Service.DeleteCoursePeriodAsync(id, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(
            CoursePeriodApplicationErrors.NotFound(id).Message,
            result.Error!.Message
        );
    }

    [Fact]
    public async Task AddAttendanceAsync_Should_Return_NotFound_When_CoursePeriod_Does_Not_Exist()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();

        AddCourseSessionAttendanceInput input =
            new AddCourseSessionAttendanceInput(
                CoursePeriodId: coursePeriodId,
                CourseSessionId: Guid.NewGuid(),
                StudentId: Guid.NewGuid(),
                Status: AttendanceStatus.Present
            );

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdWithSessionsAsync(coursePeriodId, CancellationToken))
            .ReturnsAsync((CoursePeriod?)null);

        // Act
        ApplicationResult result =
            await Service.AddAttendanceAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorTypes.NotFound, result.Error!.Type);
    }

    [Fact]
    public async Task AddAttendanceAsync_Should_Succeed_When_CoursePeriod_Exists()
    {
        // Arrange
        Guid coursePeriodId = Guid.NewGuid();
        Guid studentId = Guid.NewGuid();

        CoursePeriod coursePeriod =
            CoursePeriod.Create(
                coursePeriodId,
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateOnly.FromDateTime(DateTime.Today),
                DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                CourseFormat.Online
            );

        coursePeriod.AddSession(
            CourseFormat.Online,
            null,
            DateOnly.FromDateTime(DateTime.Today),
            TimeOnly.FromDateTime(DateTime.Now),
            TimeOnly.FromDateTime(DateTime.Now.AddHours(1))
        );

        Guid sessionId = coursePeriod.Sessions.First().Id;

        AddCourseSessionAttendanceInput input =
            new AddCourseSessionAttendanceInput(
                StudentId: studentId,
                CourseSessionId: sessionId,
                CoursePeriodId: coursePeriodId,
                Status: AttendanceStatus.Present
            );

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdWithSessionsAsync(coursePeriodId, CancellationToken))
            .ReturnsAsync(coursePeriod);

        // Act
        ApplicationResult result =
            await Service.AddAttendanceAsync(input, CancellationToken);

        // Assert
        Assert.True(result.IsSuccess);

        CoursePeriodRepositoryMock.Verify(
            r => r.AddSessionAttendanceAsync(coursePeriod, CancellationToken),
            Times.Once);

        UnitOfWorkMock.Verify(
            u => u.SaveChangesAsync(CancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task AddResourceAsync_Should_Return_NotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        AddCoursePeriodResourceInput input =
            new AddCoursePeriodResourceInput(
                CoursePeriodId: id,
                Title: "Title",
                Url: "Url",
                Description: "Desc"
            );

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdWithResourcesAsync(id, CancellationToken))
            .ReturnsAsync((CoursePeriod?)null);

        // Act
        ApplicationResult result =
            await Service.AddResourceAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorTypes.NotFound, result.Error!.Type);
    }

    [Fact]
    public async Task AddReviewAsync_Should_Return_NotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        AddCoursePeriodReviewInput input =
            new AddCoursePeriodReviewInput(
                CoursePeriodId: id,
                StudentId: Guid.NewGuid(),
                Rating: 5,
                Comment: "Bra"
            );

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdWithReviewsAsync(id, CancellationToken))
            .ReturnsAsync((CoursePeriod?)null);

        // Act
        ApplicationResult result =
            await Service.AddReviewAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorTypes.NotFound, result.Error!.Type);
    }

    [Fact]
    public async Task AddSessionAsync_Should_Return_NotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        AddCourseSessionInput input =
            new AddCourseSessionInput(
                CoursePeriodId: id,
                Format: CourseFormat.Online,
                ClassroomId: null,
                Date: DateOnly.FromDateTime(DateTime.Today),
                StartTime: TimeOnly.FromDateTime(DateTime.Now),
                EndTime: TimeOnly.FromDateTime(DateTime.Now.AddHours(1))
            );

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdWithSessionsAsync(id, CancellationToken))
            .ReturnsAsync((CoursePeriod?)null);

        // Act
        ApplicationResult result =
            await Service.AddSessionAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorTypes.NotFound, result.Error!.Type);
    }

    [Fact]
    public async Task SetCoursePeriodGradeAsync_Should_Return_NotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        SetGradeInput input =
            new SetGradeInput(
                CoursePeriodId: id,
                StudentId: Guid.NewGuid(),
                Grade: Grade.A
            );

        CoursePeriodRepositoryMock
            .Setup(r => r.GetByIdWithEnrollmentsAsync(id, CancellationToken))
            .ReturnsAsync((CoursePeriod?)null);

        // Act
        ApplicationResult result =
            await Service.SetCoursePeriodGradeAsync(input, CancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorTypes.NotFound, result.Error!.Type);
    }
}
