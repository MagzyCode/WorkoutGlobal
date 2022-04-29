using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.DatabaseContext;
using WorkoutGlobal.Api.Repositories.BaseRepositories;
using WorkoutGlobal.Api.Repositories.HealthRepository;

namespace WorkoutGlobal.Api.Extensions
{
    /// <summary>
    /// Base class for all extension services.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure database settings.
        /// </summary>
        /// <param name="services">Project services.</param>
        /// <param name="configuration">Project configuration.</param>
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<WorkoutGlobalContext>(
                opts => opts.UseSqlServer(configuration.GetConnectionString("WorkoutGlobalConnectionString"),
                b => b.MigrationsAssembly("WorkoutGlobal.Api")));

        /// <summary>
        /// Configure instances of repository classes.
        /// </summary>
        /// <param name="services">Project services.</param>
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHealthRepository, HealthRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
