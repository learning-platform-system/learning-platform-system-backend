using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.Courses;

public class Course
{
    // Listan: skapas direkt (new), är muterbar (readonly), kan aldrig bli null (new), går ej att göra new på -->är alltid samma instans (readonly)
    private readonly List<CoursePeriod> _coursePeriods = new();

    public const int CourseTitleMaxLength = 200;
    public const int CourseDescriptionMaxLength = 2000;
    public const int CreditsMinValue = 1;
    public const int CreditsMaxValue = 30;

    public Guid Id { get; private set; }
    public Guid SubcategoryId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int Credits { get; private set; }
    // immutabel egenskap som bara är en exponering av den privata listan, så att den inte kan ändras utanför klassen
    public IReadOnlyCollection<CoursePeriod> CoursePeriods => _coursePeriods;

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

    // this.Id = id på den Course som just nu kör metoden
    public void AddCoursePeriod(Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        CoursePeriod coursePeriod = CoursePeriod.Create(this.Id, teacherId, startDate, endDate, format);
        _coursePeriods.Add(coursePeriod);
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
