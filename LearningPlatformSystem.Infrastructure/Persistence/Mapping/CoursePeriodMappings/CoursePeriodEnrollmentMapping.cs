using LearningPlatformSystem.Domain.CoursePeriodEnrollments;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

namespace LearningPlatformSystem.Infrastructure.Persistence.Mapping.CoursePeriodMappings;

public static class CoursePeriodEnrollmentMapping
{
    public static IEnumerable<CoursePeriodEnrollment> ToDomainModel(this IEnumerable<CoursePeriodEnrollmentEntity> entities)
    {
        return entities.Select(entity => CoursePeriodEnrollment.Rehydrate(
            coursePeriodId: entity.CoursePeriodId,
            studentId: entity.StudentId,
            grade: entity.Grade
        ));
    }
}
