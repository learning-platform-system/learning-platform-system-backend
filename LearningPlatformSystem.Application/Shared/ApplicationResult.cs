namespace LearningPlatformSystem.Application.Shared;

public sealed record ApplicationResult
{
    public bool IsSuccess { get; }
    public ApplicationResultError? Error { get; }

    private ApplicationResult(bool isSuccess, ApplicationResultError? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static ApplicationResult Success() => new(true);

    public static ApplicationResult Fail(ApplicationResultError error) =>
        new ApplicationResult(false, error);
}

public sealed record ApplicationResult<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public ApplicationResultError? Error { get; }

    private ApplicationResult(bool isSuccess, T? data = default, ApplicationResultError? error = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    public static ApplicationResult<T> Success(T data) =>
        new(true, data);

    public static ApplicationResult<T> Fail(ApplicationResultError error) =>
        new(false, default, error);
}
