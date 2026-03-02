namespace LearningPlatformSystem.Application.Shared;

public class PersistenceErrors
{
    public static ApplicationResultError SaveFailed(string message) =>
            new ApplicationResultError(ErrorTypes.Unexpected, message);
}
