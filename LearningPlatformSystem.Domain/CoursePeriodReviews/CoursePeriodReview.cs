using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CoursePeriodReviews;

public class CoursePeriodReview
{
    public const int CommentMaxLength = 500;

    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; } 
    public Guid CoursePeriodId { get; private set; }
    public Rating Rating { get; } = null!;
    public string? Comment { get; private set; }

    private CoursePeriodReview(Guid id, Guid studentId, Guid coursePeriodId, Rating rating, string? comment)
    {
        Id = id;
        StudentId = studentId;
        CoursePeriodId = coursePeriodId;
        Rating = rating;
        Comment = comment;
    }

    internal static CoursePeriodReview Create(Guid studentId, Guid coursePeriodId, Rating rating, string? comment)
    {
        string? normalizedComment = DomainValidator.ValidateOptionalString(comment, CommentMaxLength, 
            CoursePeriodReviewErrors.CommentIsTooLong(CommentMaxLength));

        DomainValidator.ValidateRequiredGuid(studentId, CoursePeriodReviewErrors.StudentIdIsRequired);

        DomainValidator.ValidateRequiredGuid(coursePeriodId, CoursePeriodReviewErrors.CoursePeriodIdIsRequired);

        Guid id = Guid.NewGuid();

        return new CoursePeriodReview(id, studentId, coursePeriodId, rating, normalizedComment);
    }
}
