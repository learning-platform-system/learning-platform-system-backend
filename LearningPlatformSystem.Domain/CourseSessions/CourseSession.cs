namespace LearningPlatformSystem.Domain.CourseSessions;

public class CourseSession
{
    public Guid Id { get; private set; }
    public Guid CoursePeriodId { get; private set; }
    public Guid  ClassroomId { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }


    private CourseSession(Guid id, Guid coursePeriodId, Guid classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        Id = id;
        CoursePeriodId = coursePeriodId;
        ClassroomId = classroomId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }

    public static CourseSession Create(Guid coursePeriodId, Guid classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        ValidateSessionTimes(startTime, endTime);
        ValidateRequiredIds(coursePeriodId, classroomId);

        Guid id = Guid.NewGuid();
        CourseSession courseSession = new(id, coursePeriodId, classroomId, date, startTime, endTime);

        return courseSession;
    }

    // private eftersom validering körs innan objektet finns och behöver inte nås utifrån klassen
    private static void ValidateSessionTimes(TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime <= startTime)
        {
            throw new CourseSessionEndTimeMustBeAfterStartTime();
        }
    }

    // eftersom Guid är en struct (defaultvärde) och inte nullable, så kan den inte vara null
    private static void ValidateRequiredIds(Guid coursePeriodId, Guid classroomId)
    {
        if (coursePeriodId == Guid.Empty)
        {
            throw new CoursePeriodIdIsRequired();
        }

        if (classroomId == Guid.Empty)
        {
            throw new ClassroomIdIsRequired();
        }
    }
}
