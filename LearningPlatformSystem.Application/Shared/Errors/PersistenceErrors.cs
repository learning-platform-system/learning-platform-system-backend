using LearningPlatformSystem.Application.Shared.Results;

namespace LearningPlatformSystem.Application.Shared.Errors;

public class PersistenceErrors
{
    public static ResultError SaveFailed(string message) =>
            new ResultError(ErrorTypes.Unexpected, message);
}
