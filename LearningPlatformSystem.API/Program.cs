using LearningPlatformSystem.API.Campuses;
using LearningPlatformSystem.API.Categories;
using LearningPlatformSystem.API.Classrooms;
using LearningPlatformSystem.API.CoursePeriods;
using LearningPlatformSystem.API.Courses;
using LearningPlatformSystem.API.Extensions;
using LearningPlatformSystem.API.Students;
using LearningPlatformSystem.API.Teachers;
using LearningPlatformSystem.Application;
using LearningPlatformSystem.Infrastructure;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
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

builder.Services.AddMemoryCache();

var services = builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Environment.IsDevelopment());



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Course Online API");
    options.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<LearningPlatformDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapClassroomEndpoints();
app.MapCategoryEndpoints();
app.MapCourseEndpoints();
app.MapCoursePeriodEndpoints();
app.MapTeacherEndpoints();
app.MapStudentEndpoints();
app.MapCampusEndpoints();


app.Run();


