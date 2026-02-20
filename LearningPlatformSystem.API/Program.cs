using LearningPlatformSystem.Application;
using LearningPlatformSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
var services = builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();


app.UseHttpsRedirection();


app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.Run();


