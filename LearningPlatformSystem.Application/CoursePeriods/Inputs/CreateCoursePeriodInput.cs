using LearningPlatformSystem.Domain.Shared.Enums;

namespace LearningPlatformSystem.Application.CoursePeriods.Inputs;

public sealed record CreateCoursePeriodInput(Guid CourseId, Guid TeacherId, Guid? CampusId, DateOnly StartDate, DateOnly EndDate, CourseFormat Format);

