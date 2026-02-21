namespace LearningPlatformSystem.Application.Shared;

public sealed record ApplicationResult
{
    public bool Success { get; }
    public ApplicationResultError? Error { get; }

    private ApplicationResult(bool success, ApplicationResultError? error = null)
    {
        Success = success;
        Error = error;
    }

    public static ApplicationResult Ok() => new(true);

    public static ApplicationResult Fail(ApplicationResultError error) =>
        new ApplicationResult(false, error);
}

public sealed record Result<T>
{
    public bool Success { get; }
    public T? Data { get; }
    public ApplicationResultError? Error { get; }

    private Result(bool success, T? data = default, ApplicationResultError? error = null)
    {
        Success = success;
        Data = data;
        Error = error;
    }

    public static Result<T> Ok(T data) =>
        new(true, data);

    public static Result<T> Fail(ApplicationResultError error) =>
        new(false, default, error);
}
