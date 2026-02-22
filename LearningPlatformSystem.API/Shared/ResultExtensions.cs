using LearningPlatformSystem.Application.Shared;

namespace LearningPlatformSystem.API.Shared;

public static class ResultExtensions
{
    // IResult - interface i ASP.NET minimal API, som innehåller Results.
    // Func - en delegerad typ (callback), representerar en metod som inte tar några argument och returnerar ett värde av typen IResult.
    public static IResult ToHttpFailResult(this ApplicationResult result)
    {

        switch (result.Error!.Type)
        {
            case ErrorTypes.NotFound:
                return Results.NotFound(result.Error.Message);

            case ErrorTypes.BadRequest:
                return Results.BadRequest(result.Error.Message);

            case ErrorTypes.Conflict:
                return Results.Conflict(result.Error.Message);

            case ErrorTypes.Unexpected:
                return Results.Problem(result.Error.Message);

            default:
                return Results.Problem("Ett okänt fel inträffade");
        }
    }
}
