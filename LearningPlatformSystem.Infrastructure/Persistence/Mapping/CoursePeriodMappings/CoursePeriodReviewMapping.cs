using LearningPlatformSystem.Domain.CoursePeriodReviews;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

namespace LearningPlatformSystem.Infrastructure.Persistence.Mapping.CoursePeriodMappings;

public static class CoursePeriodReviewMapping
{
    public static IEnumerable<CoursePeriodReview> ToDomainModel(this IEnumerable<CoursePeriodReviewEntity> entities)
    {
        IEnumerable<CoursePeriodReview> reviews = entities.Select(entity => 
        CoursePeriodReview.Rehydrate(entity.Id, entity.StudentId, entity.CoursePeriodId, entity.Rating, entity.Comment));
        return reviews;
    }
}
