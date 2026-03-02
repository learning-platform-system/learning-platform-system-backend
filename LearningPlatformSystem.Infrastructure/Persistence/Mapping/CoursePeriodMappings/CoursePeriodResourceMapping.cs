using LearningPlatformSystem.Domain.CoursePeriodResources;
using LearningPlatformSystem.Infrastructure.Persistence.EFC.Entities;

namespace LearningPlatformSystem.Infrastructure.Persistence.Mapping.CoursePeriodMappings;

public static class CoursePeriodResourceMapping
{
    public static IEnumerable<CoursePeriodResource> ToDomainModel(this IEnumerable<CoursePeriodResourceEntity> entities)
    {
        return entities.Select(resourceEntity =>
        CoursePeriodResource.Rehydrate(resourceEntity.Id, resourceEntity.CoursePeriodId, resourceEntity.Title, resourceEntity.Url, resourceEntity.Description));
    }
}
