namespace LearningPlatformSystem.Application.Shared.Results;

public sealed record Result
{
    public bool Success { get; }
    public ResultError? Error { get; }

    private Result(bool success, ResultError? error = null)
    {
        Success = success;
        Error = error;
    }

    public static Result Ok() => new(true);

    public static Result Fail(ResultError error) =>
        new Result(false, error);
}

public sealed record Result<T>
{
    public bool Success { get; }
    public T? Data { get; }
    public ResultError? Error { get; }

    private Result(bool success, T? data = default, ResultError? error = null)
    {
        Success = success;
        Data = data;
        Error = error;
    }

    public static Result<T> Ok(T data) =>
        new(true, data);

    public static Result<T> Fail(ResultError error) =>
        new(false, default, error);
}
