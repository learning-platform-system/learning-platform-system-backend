using LearningPlatformSystem.API.Classrooms;
using LearningPlatformSystem.API.Classrooms.Create;
using LearningPlatformSystem.Application;
using LearningPlatformSystem.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// För att kunna serialisera enum som string i JSON-request/response
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters
        .Add(new JsonStringEnumConverter());
});

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Course Online API");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();


app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapClassroomEndpoints();


app.Run();


