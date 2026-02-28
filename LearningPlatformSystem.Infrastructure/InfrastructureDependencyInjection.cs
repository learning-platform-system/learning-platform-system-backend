using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Campuses;
using LearningPlatformSystem.Domain.Categories;
using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Domain.CoursePeriods;
using LearningPlatformSystem.Domain.Courses;
using LearningPlatformSystem.Domain.Students;
using LearningPlatformSystem.Domain.Teachers;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningPlatformSystem.Infrastructure;

public static class InfrastructureDependencyInjection
{
    // configuration används för att läsa in inställningar från appsettings.json ex connectionstring för databasen
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        if (isDevelopment)
        {
            // SQLite
            string? connectionString = configuration.GetConnectionString("DevelopmentDB")
                    ?? "Data Source=:memory:;Cache=Shared";

            SqliteConnection sqliteInMemoryConnection =
                new SqliteConnection(connectionString);

            sqliteInMemoryConnection.Open();

            services.AddSingleton(sqliteInMemoryConnection);

            services.AddDbContext<LearningPlatformDbContext>((serviceProvider, options) =>
            {
                SqliteConnection connection =
                    serviceProvider.GetRequiredService<SqliteConnection>();

                options.UseSqlite(connection);
            });
        }
        else
        {
            // SQL Server
            string? connectionString = configuration.GetConnectionString("ProductionDB")
             ?? throw new InvalidOperationException("Missing ProductionDB connectionstring");

            services.AddDbContext<LearningPlatformDbContext>(options => options.UseSqlServer(connectionString));
        }

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LearningPlatformDbContext>());

        services.AddScoped<IClassroomRepository, ClassroomRepository>();    
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICoursePeriodRepository, CoursePeriodRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ICampusRepository, CampusRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();


        return services;
    }

}
