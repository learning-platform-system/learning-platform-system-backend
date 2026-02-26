using LearningPlatformSystem.Domain.CourseSessionAttendances;
using LearningPlatformSystem.Domain.Shared.Enums;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Validators;

namespace LearningPlatformSystem.Domain.CourseSessions;

public sealed class CourseSession
{
    private readonly List<CourseSessionAttendance> _attendances = new();

    public Guid Id { get; private set; }
    public Guid CoursePeriodId { get; private set; }
    public CourseFormat Format { get; private set; }
    public Guid?  ClassroomId { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public IReadOnlyCollection<CourseSessionAttendance> Attendances => _attendances;


    private CourseSession(Guid id, CourseFormat format, Guid coursePeriodId, Guid? classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        Id = id;
        CoursePeriodId = coursePeriodId;
        Format = format;
        ClassroomId = classroomId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }

    // skapande via CoursePeriod
    internal static CourseSession Create(Guid coursePeriodId, CourseFormat format, Guid? classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        ValidateSessionTimes(startTime, endTime);

        DomainValidator.ValidateRequiredGuid(coursePeriodId, CourseSessionErrors.CoursePeriodIdIsRequired);

        ValidateSessionFormat(classroomId, format);

         Guid id = Guid.NewGuid();
        CourseSession courseSession = new(id, format, coursePeriodId, classroomId, date, startTime, endTime);

        return courseSession;
    }

    internal static CourseSession Rehydrate(Guid id, Guid coursePeriodId, CourseFormat format, Guid? classroomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        CourseSession courseSession = new(id, format, coursePeriodId, classroomId, date, startTime, endTime);

        return courseSession;
    }

    private static void ValidateSessionTimes(TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime <= startTime)
        {
            throw new DomainException(CourseSessionErrors.CourseSessionEndTimeMustBeAfterStartTime);
        }
    }

    public void AddAttendance(Guid studentId, AttendanceStatus status)
    {
        DomainValidator.ValidateRequiredGuid(studentId, CourseSessionAttendanceErrors.StudentIdIsRequired);

        if (_attendances.Any(attendance => attendance.StudentId == studentId))
        {
            throw new DomainException(CourseSessionAttendanceErrors.AttendanceAlreadyRegistered);
        }

        CourseSessionAttendance attendance = CourseSessionAttendance.Create(studentId, this.Id, status);
        _attendances.Add(attendance);
    }

    private static void ValidateSessionFormat(Guid? classroomId, CourseFormat format)
    {
        if (format == CourseFormat.Onsite)
        {
            if (classroomId is null)
                throw new DomainException(CourseSessionErrors.ClassroomIdIsRequiredForOnsiteSession);

            if (classroomId == Guid.Empty)
                throw new DomainException(CourseSessionErrors.ClassroomIdIsRequiredForOnsiteSession);
        }

        if (format == CourseFormat.Online) 
        {
            if (classroomId is not null)
                throw new DomainException(CourseSessionErrors.ClassroomNotAllowedForOnlineSession);
        }
    }
}
