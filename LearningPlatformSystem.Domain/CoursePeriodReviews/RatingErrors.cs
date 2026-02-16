namespace LearningPlatformSystem.Domain.CoursePeriodReviews;

public static class RatingErrors
{
    public static string RatingValueOutOfRange(int ratingMinValue, int ratingMaxValue) => $"Betyget måste vara mellan {ratingMinValue} och {ratingMaxValue}.";
}