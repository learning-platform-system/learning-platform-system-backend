using LearningPlatformSystem.API.Classrooms;
using LearningPlatformSystem.API.Extensions;
using LearningPlatformSystem.Application;
using LearningPlatformSystem.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// För att kunna serialisera enum som string i JSON-request/response body
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters
        .Add(new JsonStringEnumConverter());
});

// visa string i Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
});

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
var services = builder.Services.AddApplication().AddInfrastructure(builder.Configuration);



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Course Online API");
    options.RoutePrefix = string.Empty;
});

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();


app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapClassroomEndpoints();


app.Run();


