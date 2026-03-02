using LearningPlatformSystem.Domain.Shared.Enums;

namespace LearningPlatformSystem.Application.CoursePeriods.Inputs;

public sealed record AddCourseSessionInput(Guid CoursePeriodId, CourseFormat Format, Guid? ClassroomId, DateOnly Date, TimeOnly StartTime, TimeOnly EndTime);

