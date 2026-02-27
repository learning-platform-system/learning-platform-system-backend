using LearningPlatformSystem.Domain.Shared.Enums;

namespace LearningPlatformSystem.API.CoursePeriods.AddCourseSession;

public sealed record AddCourseSessionRequest(
    string Format, 
    Guid? ClassroomId, 
    DateOnly Date, 
    TimeOnly StartTime, 
    TimeOnly EndTime
);
