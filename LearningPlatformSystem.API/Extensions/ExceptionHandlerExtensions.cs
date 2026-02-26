using LearningPlatformSystem.Application.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace LearningPlatformSystem.API.Extensions;
/* (UseExceptionHandler - inbyggd ASP.NET) 
   Global felhantering:
   - DomainException => 400 (bruten affärsregel)
   - PersistenceException => 500 (tekniskt fel vid datalagring)
   - Övriga fel => 500
   Detta gör att services kan fokusera på affärslogik och returnera ApplicationResult
   för förväntade applikationsfel (t.ex. NotFound, Conflict).
*/
public static class ExceptionHandlerExtensions
{
    public static void UseGlobalExceptionHandling(this WebApplication app)
    {
        bool isDevelopment = app.Environment.IsDevelopment();

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerFeature>()?
                    .Error;

                context.Response.ContentType = "application/json";

                switch (exception)
                {
                    // Ogiltig JSON / model binding (t.ex. Guid-format)
                    case BadHttpRequestException or JsonException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = isDevelopment && exception is not null
                                ? exception.Message
                                : "Ogiltig request. Kontrollera JSON och datatyper (t.ex. Guid-format)."
                        });
                        break;

                    // DomainException från domain-lagret (bruten affärsregel)
                    case DomainException domainEx:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = domainEx.Message
                        });
                        break;

                    // DbUpdateException från SaveChangesAsync
                    case PersistenceException:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = "Ett fel uppstod vid sparning."
                        });
                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = "Ett okänt fel uppstod"
                        });
                        break;
                }
            });
        });
    }
}
