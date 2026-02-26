using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.Courses;

public sealed class Course
{
    public const int CourseTitleMaxLength = 200;
    public const int CourseDescriptionMaxLength = 2000;
    public const int CreditsMinValue = 1;
    public const int CreditsMaxValue = 30;

    public Guid Id { get; private set; }
    public Guid SubcategoryId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int Credits { get; private set; }

    private Course(Guid id, Guid subcategoryId, string title, string? description, int credits)
    {
        Id = id;
        Title = title;
        Description = description;
        Credits = credits;
        SubcategoryId = subcategoryId;
    }

    public static Course Create(Guid subcategoryId, string title, string? description, int credits)
    {
        string normalizedTitle = DomainValidator.ValidateRequiredString(title, CourseTitleMaxLength, 
            CourseErrors.CourseTitleIsRequired, CourseErrors.CourseTitleIsTooLong(CourseTitleMaxLength)); 

        ValidateCredits(credits);

        string? normalizedDescription = DomainValidator.ValidateOptionalString(description, CourseDescriptionMaxLength, 
            CourseErrors.CourseDescriptionIsTooLong(CourseDescriptionMaxLength));

        DomainValidator.ValidateRequiredGuid(subcategoryId, CourseErrors.SubcategoryIdIsRequired);

        Guid id = Guid.NewGuid();

        return new Course(id, subcategoryId, normalizedTitle, normalizedDescription, credits);
    }

    internal static Course Rehydrate(Guid id, Guid subcategoryId, string title, string? description, int credits)
    {
        return new Course(id, subcategoryId, title, description, credits);
    }

    private static void ValidateCredits(int credits)
    {
        if (credits < CreditsMinValue || credits > CreditsMaxValue)
        {
            throw new DomainException(
                CourseErrors.CreditsValueOutOfRange(CreditsMinValue, CreditsMaxValue));
        }
    }
}
