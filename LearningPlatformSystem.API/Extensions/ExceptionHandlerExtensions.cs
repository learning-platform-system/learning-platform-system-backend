using LearningPlatformSystem.Application.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace LearningPlatformSystem.API.Extensions;
/* Inbyggd felhantering i ASP.Net Core. Fångar upp tekniska fel som inte hanteras någon annanstans i appen.
   Presentations ansvar är att se till att tekniska oväntade fel 500 (t.ex. databasfel) returnerar rätt HTTP-svar. 
   Behöver då bara try catch för DomainExceptions i Application (affärsregelbrott).*/
public static class ExceptionHandlerExtensions
{
    public static void UseGlobalExceptionHandling(this WebApplication app)
    {
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
