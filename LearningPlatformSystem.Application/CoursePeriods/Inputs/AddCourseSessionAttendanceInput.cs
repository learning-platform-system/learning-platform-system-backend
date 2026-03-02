using LearningPlatformSystem.Domain.CourseSessionAttendances;

namespace LearningPlatformSystem.Application.CoursePeriods.Inputs;

public sealed record AddCourseSessionAttendanceInput(Guid StudentId, Guid CourseSessionId, Guid CoursePeriodId, AttendanceStatus Status);

