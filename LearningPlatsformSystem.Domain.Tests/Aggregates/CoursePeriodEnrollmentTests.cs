using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.Aggregates;

public class CoursePeriodEnrollmentTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenStudentIdIsEmpty()
    {
        // Arrange
        Guid studentId = Guid.Empty;
        Guid coursePeriodId = Guid.NewGuid();

        // Act
        Action act = () => CoursePeriodEnrollment.Create(studentId, coursePeriodId);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodEnrollmentErrors.StudentIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenCoursePeriodIdIsEmpty()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.Empty;

        // Act
        Action act = () => CoursePeriodEnrollment.Create(studentId, coursePeriodId);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodEnrollmentErrors.CoursePeriodIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldSetGradeToNotSet_WhenValid()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();

        // Act
        CoursePeriodEnrollment enrollment =
            CoursePeriodEnrollment.Create(studentId, coursePeriodId);

        // Assert
        Assert.Equal(studentId, enrollment.StudentId);
        Assert.Equal(coursePeriodId, enrollment.CoursePeriodId);
        Assert.Equal(Grade.NotSet, enrollment.Grade);
    }

    [Fact]
    public void SetGrade_ShouldSetGrade_WhenNotPreviouslySet()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();
        CoursePeriodEnrollment enrollment =
            CoursePeriodEnrollment.Create(studentId, coursePeriodId);

        // Act
        enrollment.SetGrade(Grade.A);

        // Assert
        Assert.Equal(Grade.A, enrollment.Grade);
    }

    [Fact]
    public void SetGrade_ShouldThrowDomainException_WhenGradeAlreadySet()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();
        CoursePeriodEnrollment enrollment =
            CoursePeriodEnrollment.Create(studentId, coursePeriodId);

        enrollment.SetGrade(Grade.A);

        // Act
        Action act = () => enrollment.SetGrade(Grade.B);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodEnrollmentErrors.GradeAlreadySet, exception.Message);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();
        Grade grade = Grade.C;

        // Act
        CoursePeriodEnrollment enrollment =
            CoursePeriodEnrollment.Rehydrate(studentId, coursePeriodId, grade);

        // Assert
        Assert.Equal(studentId, enrollment.StudentId);
        Assert.Equal(coursePeriodId, enrollment.CoursePeriodId);
        Assert.Equal(grade, enrollment.Grade);
    }
}
