namespace LearningPlatformSystem.Application.Shared;

public static class ApplicationErrors
{
    public static ApplicationResultError BadRequest(string message)
    {
        return new ApplicationResultError(ErrorTypes.BadRequest, message);
    }
}
