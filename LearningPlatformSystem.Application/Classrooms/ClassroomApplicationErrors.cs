using LearningPlatformSystem.Application.Shared;
using System.Xml.Linq;

namespace LearningPlatformSystem.Application.Classrooms;

public static class ClassroomApplicationErrors
{
    public static ApplicationResultError CouldNotBeFound(Guid classroomId) =>
        new ApplicationResultError(ErrorTypes.NotFound, $"Klassrummet med id {classroomId} kunde inte hittas.");

    public static ApplicationResultError NameAlreadyExists(string name)
    {
       return new ApplicationResultError(ErrorTypes.Conflict, $"Ett klassrum med namnet {name} finns redan.");
    }
}
