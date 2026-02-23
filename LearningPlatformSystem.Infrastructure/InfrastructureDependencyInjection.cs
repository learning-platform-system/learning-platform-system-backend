using LearningPlatformSystem.Application.Shared;
using LearningPlatformSystem.Domain.Classrooms;
using LearningPlatformSystem.Infrastructure.Persistence.EFC;
using LearningPlatformSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningPlatformSystem.Infrastructure;

public static class InfrastructureDependencyInjection
{
    // configuration används för att läsa in inställningar från appsettings.json eller andra konfigurationskällor, ex. connection string för databasen.
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LearningPlatformDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });


        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<LearningPlatformDbContext>());

        //services.AddScoped<IUnitOfWork, LearningPlatformDbContext>();

        services.AddScoped<IClassroomRepository, ClassroomRepository>();


        return services;
    }

}
