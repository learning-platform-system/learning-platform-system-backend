using LearningPlatformSystem.Domain.CourseSessions;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

namespace LearningPlatformSystem.Infrastructure.Persistence.Mapping.CoursePeriodMappings;

public static class CourseSessionMapping
{
    public static IEnumerable<CourseSession> ToDomainModel(this IEnumerable<CourseSessionEntity> entities)
    {
        return entities.Select(entity => CourseSession.Rehydrate(
            id: entity.Id,
            coursePeriodId: entity.CoursePeriodId,
            format: entity.Format,
            classroomId: entity.ClassroomId,
            date: entity.Date,
            startTime: entity.StartTime,
            endTime: entity.EndTime
        ));
    }
}
