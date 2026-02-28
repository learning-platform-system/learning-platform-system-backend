using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformSystem.Infrastructure.Integrationtests.Integration.Persistence.Repositories;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class CoursePeriodRepositoryTests(SqliteInMemoryFixture _fixture)
{
    [Fact]
    public async Task AddAsync_Should_Add_CoursePeriod()
    {
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CoursePeriodRepository repository = new(context);
        CoursePeriod coursePeriod = await CreateCoursePeriodAsync(context, ct);

        await repository.AddAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        bool exists = await context.CoursePeriods
            .AnyAsync(x => x.Id == coursePeriod.Id, ct);

        Assert.True(exists);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_CoursePeriod()
    {
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CoursePeriodRepository repository = new(context);
        CoursePeriod coursePeriod = await CreateCoursePeriodAsync(context, ct);

        await repository.AddAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        CoursePeriod? result =
            await repository.GetByIdAsync(coursePeriod.Id, ct);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task RemoveAsync_Should_Remove_CoursePeriod()
    {
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CoursePeriodRepository repository = new(context);
        CoursePeriod coursePeriod = await CreateCoursePeriodAsync(context, ct);

        await repository.AddAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        bool removed =
            await repository.RemoveAsync(coursePeriod.Id, ct);

        await context.SaveChangesAsync(ct);

        bool exists = await context.CoursePeriods
            .AnyAsync(x => x.Id == coursePeriod.Id, ct);

        Assert.True(removed);
        Assert.False(exists);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Dates()
    {
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CoursePeriodRepository repository = new(context);
        CoursePeriod coursePeriod = await CreateCoursePeriodAsync(context, ct);

        await repository.AddAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        CoursePeriod updated = CoursePeriod.Create(
            coursePeriod.Id,
            coursePeriod.CourseId,
            coursePeriod.TeacherId,
            DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Today.AddDays(10)),
            coursePeriod.Format);

        await repository.UpdateAsync(updated, ct);
        await context.SaveChangesAsync(ct);

        var entity = await context.CoursePeriods
            .SingleAsync(x => x.Id == coursePeriod.Id, ct);

        Assert.Equal(DateOnly.FromDateTime(DateTime.Today.AddDays(10)), entity.EndDate);
    }

    [Fact]
    public async Task AddSessionAsync_Should_Add_Session()
    {
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CoursePeriodRepository repository = new(context);
        CoursePeriod coursePeriod = await CreateCoursePeriodAsync(context, ct);

        await repository.AddAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        Guid classroomId =
            await CreateClassroomAsync(context, ct);

        coursePeriod.AddSession(
            CourseFormat.Onsite,
            classroomId,
            DateOnly.FromDateTime(DateTime.Today),
            TimeOnly.FromDateTime(DateTime.Now),
            TimeOnly.FromDateTime(DateTime.Now.AddHours(1)));

        await repository.AddSessionAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        bool exists =
            await context.CourseSessions
                .AnyAsync(x => x.CoursePeriodId == coursePeriod.Id, ct);

        Assert.True(exists);
    }


    [Fact]
    public async Task AddEnrollmentAsync_Should_Add_Enrollment()
    {
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CoursePeriodRepository repository = new(context);
        CoursePeriod coursePeriod = await CreateCoursePeriodAsync(context, ct);

        await repository.AddAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        Guid studentId = await CreateStudentAsync(context, ct);

        coursePeriod.EnrollStudent(studentId);

        await repository.AddEnrollmentAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        bool exists = await context.CoursePeriodEnrollments
            .AnyAsync(x => x.StudentId == studentId, ct);

        Assert.True(exists);
    }

    [Fact]
    public async Task GetByCourseIdAsync_Should_Return_List()
    {
        CancellationToken ct = CancellationToken.None;

        await _fixture.ClearDatabaseAsync();
        await using LearningPlatformDbContext context = _fixture.CreateContext();

        CoursePeriodRepository repository = new(context);
        CoursePeriod coursePeriod = await CreateCoursePeriodAsync(context, ct);

        await repository.AddAsync(coursePeriod, ct);
        await context.SaveChangesAsync(ct);

        IReadOnlyList<CoursePeriod> result =
            await repository.GetByCourseIdAsync(coursePeriod.CourseId, ct);

        Assert.Single(result);
    }

    private static async Task<Guid> CreateClassroomAsync(
        LearningPlatformDbContext context,
        CancellationToken ct)
    {
        Guid classroomId = Guid.NewGuid();

        Classroom classroom =
            Classroom.Create(
                classroomId,
                "A701",                
                30,                     
                ClassroomType.Laboratory);

        ClassroomEntity entity = new()
        {
            Id = classroom.Id,
            Name = classroom.Name,
            Capacity = classroom.Capacity,
            Type = classroom.Type
        };

        await context.Classrooms.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);

        return classroomId;
    }

    private static async Task<Guid> CreateStudentAsync(
    LearningPlatformDbContext context,
    CancellationToken ct)
    {
        Guid studentId = Guid.NewGuid();

        await context.Database.ExecuteSqlInterpolatedAsync(
            $"""
        INSERT INTO Students (Id, FirstName, LastName, Email, PhoneNumber)
        VALUES ({studentId}, {"Test"}, {"Student"}, {"test@student.se"}, {"0700000000"})
        """,
            ct);

        return studentId;
    }

    private static async Task<Guid> CreateTeacherAsync(
    LearningPlatformDbContext context,
    CancellationToken ct)
    {
        Guid teacherId = Guid.NewGuid();

        await context.Database.ExecuteSqlInterpolatedAsync(
            $"""
        INSERT INTO Teachers (Id, FirstName, LastName, Email, PhoneNumber)
        VALUES ({teacherId}, {"Test"}, {"Teacher"}, {"teacher@test.se"}, {"0700000000"})
        """,
            ct);

        return teacherId;
    }

    private static async Task<Guid> CreateCourseAsync(
    LearningPlatformDbContext context,
    CancellationToken ct)
    {
        Guid courseId = Guid.NewGuid();

        await context.Database.ExecuteSqlInterpolatedAsync(
            $"""
        INSERT INTO Courses (Id, SubcategoryId, Title, Description, Credits)
        VALUES ({courseId}, {Guid.NewGuid()}, {"Test Course"}, {"Desc"}, {5})
        """,
            ct);

        return courseId;
    }

    private static async Task<CoursePeriod> CreateCoursePeriodAsync(
    LearningPlatformDbContext context,
    CancellationToken ct)
    {
        // Category
        Guid categoryId = Guid.NewGuid();
        await context.Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO Categories (Id, Name) VALUES ({categoryId}, {"Programming"})",
            ct);

        // Subcategory
        Guid subcategoryId = Guid.NewGuid();
        await context.Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO Subcategories (Id, CategoryId, Name) VALUES ({subcategoryId}, {categoryId}, {"Backend"})",
            ct);

        // Course
        Guid courseId = Guid.NewGuid();
        await context.Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO Courses (Id, SubcategoryId, Title, Description, Credits) VALUES ({courseId}, {subcategoryId}, {"Test Course"}, {"Desc"}, {5})",
            ct);

        // Teacher
        Guid teacherId = Guid.NewGuid();
        await context.Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO Teachers (Id, FirstName, LastName, Email, PhoneNumber) VALUES ({teacherId}, {"Test"}, {"Teacher"}, {"teacher@test.se"}, {"0700000000"})",
            ct);

        await context.SaveChangesAsync(ct);

        return CoursePeriod.Create(
            Guid.NewGuid(),
            courseId,
            teacherId,
            DateOnly.FromDateTime(DateTime.Today),
            DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
            CourseFormat.Onsite);
    }
}
