using LearningPlatformSystem.Domain.CourseSessionAttendances;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

namespace LearningPlatformSystem.Infrastructure.Persistence.Mapping.CoursePeriodMappings;

public static class CourseSessionAttendanceMapping
{
    public static IEnumerable<CourseSessionAttendance> ToDomainModel(this IEnumerable<CourseSessionAttendanceEntity> entities)
    {
        IEnumerable<CourseSessionAttendance> sessionAttendances = entities.Select(entity => 
        CourseSessionAttendance.Rehydrate(
            studentId: entity.StudentId,
            courseSessionId: entity.CourseSessionId,
            status: entity.Status
        ));


        return sessionAttendances;
    }
}
