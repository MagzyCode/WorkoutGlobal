using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.AuthenticationManagerContracts;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Repositories.AuthorizationRepositories;
using WorkoutGlobal.Api.Repositories.BaseRepositories;
using WorkoutGlobal.Api.Repositories.HealthRepository;
using WorkoutGlobal.Api.Repositories.ModelsRepositories;

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
            services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICommentsBlockRepository, CommentsBlockRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        /// <summary>
        /// Configure instances of attributes.
        /// </summary>
        /// <param name="services">Project services.</param>
        public static void ConfigureAttributes(this IServiceCollection services)
        {
            services.AddScoped<ModelValidationFilterAttribute>();
        }

        /// <summary>
        /// Configure identity.
        /// </summary>
        /// <param name="services">Project services.</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserCredentials>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10; 
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
                builder.Services);
            builder.AddEntityFrameworkStores<WorkoutGlobalContext>().AddDefaultTokenProviders();
        }
    }
}
