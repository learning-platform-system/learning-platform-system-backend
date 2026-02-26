using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Domain.Teachers;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningPlatformSystem.Infrastructure;

public static class InfrastructureDependencyInjection
{
    // configuration används för att läsa in inställningar från appsettings.json ex connectionstring för databasen
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LearningPlatformDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });


        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LearningPlatformDbContext>());

        services.AddScoped<IClassroomRepository, ClassroomRepository>();    
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICoursePeriodRepository, CoursePeriodRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ICampusRepository, CampusRepository>();


        return services;
    }

}
