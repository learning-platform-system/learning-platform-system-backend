using LearningPlatformSystem.Domain.Shared.Enums;

namespace LearningPlatformSystem.Application.CoursePeriods.Outputs;

public sealed record CoursePeriodOutput
(
    Guid Id,
    Guid CourseId,
    Guid TeacherId,
    Guid CampusId,
    DateOnly StartDate,
    DateOnly EndDate,
    CourseFormat Format
);
