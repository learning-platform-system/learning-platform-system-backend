using LearningPlatformSystem.Domain.Shared.Enums;

namespace LearningPlatformSystem.API.CoursePeriods.Create;

public sealed record CreateCoursePeriodRequest(Guid CourseId, Guid TeacherId, Guid CampusId, DateOnly StartDate, DateOnly EndDate, string Format);