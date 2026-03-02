using LearningPlatformSystem.Application.Shared.Exceptions;
using LearningPlatformSystem.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

                    // Mot Race-condition / unika index (t.ex. två samtidiga skapanden med samma unika värde)
                    case PersistenceException persistenceEx
                         when persistenceEx.InnerException is DbUpdateException dbEx &&
                             dbEx.InnerException is SqlException sqlEx &&
                             (sqlEx.Number == 2601 || sqlEx.Number == 2627):

                        context.Response.StatusCode = StatusCodes.Status409Conflict;

                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = "Resursen bryter mot en unikhetsregel."
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
