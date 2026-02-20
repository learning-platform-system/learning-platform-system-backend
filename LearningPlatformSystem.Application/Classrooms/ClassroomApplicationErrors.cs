using LearningPlatformSystem.Application.Shared.Results;

namespace LearningPlatformSystem.Application.Classrooms;

public static class ClassroomApplicationErrors
{
    public static ResultError ClassroomNotFound(Guid classroomId) =>
        new ResultError(ErrorTypes.NotFound, $"Klassrummet med id {classroomId} kunde inte hittas.");

    public static ResultError BadRequest(string message) => new ResultError(ErrorTypes.BadRequest, message);
    
}
