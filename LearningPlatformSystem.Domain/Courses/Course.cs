using LearningPlatformSystem.Domain.Shared;

namespace LearningPlatformSystem.Domain.Courses;

public class Course
{
    private Course() { } // parameterlös konstruktor som krävs av EF Core

    public const int CourseTitleMaxLength = 200;
    public const int CreditsMinValue = 1;
    public const int CreditsMaxValue = 30;

    public Guid Id { get; private set; }
    public Guid SubcategoryId { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public int Credits { get; private set; }
    //public List<CoursePeriod>? CoursePeriods { get; private set; }

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

        string? normalizedDescription = DomainValidator.ValidateOptionalString(description);

        DomainValidator.ValidateRequiredGuid(subcategoryId, CourseErrors.SubcategoryIdIsRequired);

        Guid id = Guid.NewGuid();
        Course course = new(id, subcategoryId, normalizedTitle, normalizedDescription, credits);

        return course;
    }

    //public void AddCoursePeriod(DateTime startDate, DateTime endDate)
    //{
    //    if (CoursePeriods == null)
    //    {
    //        CoursePeriods = new List<CoursePeriod>();
    //    }
    //    CoursePeriod coursePeriod = CoursePeriod.Create(this, startDate, endDate);
    //    CoursePeriods.Add(coursePeriod);
    //}

    private static void ValidateCredits(int credits)
    {
        if (credits < CreditsMinValue || credits > CreditsMaxValue)
        {
            throw new DomainException(
                CourseErrors.CreditsValueOutOfRange(CreditsMinValue, CreditsMaxValue));
        }
    }
}
