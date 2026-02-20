using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.ValueObjects;

namespace LearningPlatformSystem.Domain.CoursePeriodReviews;

public class Rating : ValueObject
{
    public const int RatingMinValue = 1;
    public const int RatingMaxValue = 5;

    public int Value { get; }
    
    private Rating() { } // parameterlös konstruktor som krävs av EF Core

    private Rating(int value)
    {
        Value = value;
    }

    public static Rating Create(int value) 
    {
        if (value < RatingMinValue || value > RatingMaxValue)
            throw new DomainException(RatingErrors.RatingValueOutOfRange(RatingMinValue, RatingMaxValue));

        return new Rating(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
