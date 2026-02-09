namespace LearningPlatformSystem.Domain.CoursePeriods;

public class CoursePeriod
{
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid TeacherId { get; private set; }
    public Guid? CampusId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public CourseFormat Format { get; private set; }

    //public List<CourseSession> CourseSessions { get; private set; }

    private CoursePeriod(Guid id, Guid courseId, Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        Id = id;
        CourseId = courseId;
        TeacherId = teacherId;
        StartDate = startDate;
        EndDate = endDate;
        Format = format;
    }

    public static CoursePeriod Create(Guid courseId, Guid teacherId, DateOnly startDate, DateOnly endDate, CourseFormat format)
    {
        Guid id = Guid.NewGuid();
        CoursePeriod coursePeriod = new(id, courseId, teacherId, startDate, endDate, format);

        return coursePeriod;
    }

    public void ConnectToCampus(Guid campusId) 
    {
        CampusId = campusId;
    }

}
