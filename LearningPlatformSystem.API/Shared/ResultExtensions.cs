using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Shared;

public static class ResultExtensions
{
    // IResult - interface i ASP.NET minimal API, som innehåller Results.
    public static IResult ToHttpFailResult(this ApplicationResult result)
    {
        return GetHttpResult(result.Error!.Type, result.Error.Message);
    }

    public static IResult ToHttpFailResult<T>(this ApplicationResult<T> result)
    {
        return GetHttpResult(result.Error!.Type, result.Error.Message);
    }

    private static IResult GetHttpResult(ErrorTypes errorType, string message)
    {
        switch (errorType)
        {
            case ErrorTypes.NotFound:
                return Results.NotFound(message);

            case ErrorTypes.BadRequest:
                return Results.BadRequest(message);

            case ErrorTypes.Conflict:
                return Results.Conflict(message);

            case ErrorTypes.Unexpected:
                return Results.Problem(message);

            default:
                return Results.Problem("Ett okänt fel inträffade");
        }
    }
}
