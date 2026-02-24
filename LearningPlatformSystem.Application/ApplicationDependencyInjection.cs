using LearningPlatformSystem.Application.Campuses;
using LearningPlatformSystem.Application.Categories;
using LearningPlatformSystem.Application.Classrooms;
using LearningPlatformSystem.Application.CoursePeriods;
using LearningPlatformSystem.Application.Courses;
using LearningPlatformSystem.Application.CourseSessions;
using LearningPlatformSystem.Application.Students;
using LearningPlatformSystem.Application.Teachers;
using Microsoft.Extensions.DependencyInjection;

namespace LearningPlatformSystem.Application;

public static class ApplicationDependencyInjection
{
    //configuration och ihostenvironment???
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICampusService, CampusService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IClassroomService, ClassroomService>();
        services.AddScoped<ICoursePeriodService, CoursePeriodService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseSessionService, CourseSessionService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        

        return services;
    }
}
