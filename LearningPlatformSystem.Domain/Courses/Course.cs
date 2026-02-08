namespace LearningPlatformSystem.Domain.Courses;

public class Course
{
    public const int TitleMaxLength = 200;
    
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
        string normalizedTitle = title?.Trim() ?? string.Empty;
        var normalizedDescription = description?.Trim() ?? null;

        ValidateTitle(normalizedTitle);
        ValidateCredits(credits);

        Guid id = Guid.NewGuid();
        Course course = new(id, subcategoryId, normalizedTitle, description, credits);

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
        if (credits <= 0)
        {
            throw new CourseCreditsMustBePositive();
        }
    }

    private static void ValidateTitle(string normalizedTitle)
    {
        if (string.IsNullOrWhiteSpace(normalizedTitle))
        {
            throw new CourseTitleIsRequired();
        }
        
        if (normalizedTitle.Length > TitleMaxLength)
        {
            throw new CourseTitleTooLong(TitleMaxLength);
        }
    }
}
