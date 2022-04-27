using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.DatabaseContext;
using WorkoutGlobal.Api.Repositories.BaseRepositories;
using WorkoutGlobal.Api.Repositories.HealthRepository;

namespace WorkoutGlobal.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<WorkoutGlobalContext>(
                opts => opts.UseSqlServer(configuration.GetConnectionString("WorkoutGlobalConnectionString"),
                b => b.MigrationsAssembly("WorkoutGlobal.Api")));

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHealthRepository, HealthRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
