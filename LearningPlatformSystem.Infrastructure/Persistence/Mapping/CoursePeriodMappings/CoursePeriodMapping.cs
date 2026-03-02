using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

namespace LearningPlatformSystem.Infrastructure.Persistence.Mapping.CoursePeriodMappings;

public static class CoursePeriodMapping
{
    public static CoursePeriod ToDomainModel(this CoursePeriodEntity entity)
    {
        return CoursePeriod.Rehydrate(
            id: entity.Id,
            courseId: entity.CourseId,
            teacherId: entity.TeacherId,
            startDate: entity.StartDate,
            endDate: entity.EndDate,
            format: entity.Format
        );
    }

}
