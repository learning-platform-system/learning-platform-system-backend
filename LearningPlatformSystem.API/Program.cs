using LearningPlatformSystem.Application.Extensions;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddApplication();

//AddInfrastructure(builder.Configuration);



var app = builder.Build();

app.MapOpenApi();


app.UseHttpsRedirection();


app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.Run();


