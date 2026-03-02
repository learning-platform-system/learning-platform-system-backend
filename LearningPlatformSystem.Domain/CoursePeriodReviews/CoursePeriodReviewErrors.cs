namespace LearningPlatformSystem.Domain.CoursePeriodReviews;

public static class CoursePeriodReviewErrors
{
    public static string CommentIsTooLong(int commentMaxLength) => $"Kommentaren får inte vara längre än {commentMaxLength} tecken.";

    public const string StudentIdIsRequired = "Student-ID måste anges";

    public const string CoursePeriodIdIsRequired = "Kursperiodens id mpste anges.";
}