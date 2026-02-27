using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Domain.Shared.Exceptions;

namespace LearningPlatformSystem.Domain.Tests.Aggregates;

public class CoursePeriodReviewTests
{
    [Fact]
    public void Create_ShouldThrowDomainException_WhenStudentIdIsEmpty()
    {
        // Arrange
        Guid studentId = Guid.Empty;
        Guid coursePeriodId = Guid.NewGuid();
        Rating rating = Rating.Create(5);
        string? comment = "Bra kurs";

        // Act
        Action act = () => CoursePeriodReview.Create(studentId, coursePeriodId, rating, comment);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodReviewErrors.StudentIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenCoursePeriodIdIsEmpty()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.Empty;
        Rating rating = Rating.Create(5);
        string? comment = "Bra kurs";

        // Act
        Action act = () => CoursePeriodReview.Create(studentId, coursePeriodId, rating, comment);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(CoursePeriodReviewErrors.CoursePeriodIdIsRequired, exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowDomainException_WhenCommentIsTooLong()
    {
        // Arrange
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();
        Rating rating = Rating.Create(5);
        string? comment = new string('A', CoursePeriodReview.CommentMaxLength + 1);

        // Act
        Action act = () => CoursePeriodReview.Create(studentId, coursePeriodId, rating, comment);

        // Assert
        DomainException exception = Assert.Throws<DomainException>(act);
        Assert.Equal(
            CoursePeriodReviewErrors.CommentIsTooLong(CoursePeriodReview.CommentMaxLength),
            exception.Message);
    }

    [Fact]
    public void Rehydrate_ShouldCreateInstance_WithGivenValues()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid studentId = Guid.NewGuid();
        Guid coursePeriodId = Guid.NewGuid();
        Rating rating = Rating.Create(4);
        string? comment = "Mycket bra";

        // Act
        CoursePeriodReview review = CoursePeriodReview.Rehydrate(id, studentId, coursePeriodId, rating, comment);

        // Assert
        Assert.Equal(id, review.Id);
        Assert.Equal(studentId, review.StudentId);
        Assert.Equal(coursePeriodId, review.CoursePeriodId);
        Assert.Equal(rating, review.Rating);
        Assert.Equal(comment, review.Comment);
    }
}
