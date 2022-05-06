using WorkoutGlobal.UI.Filters.ActionFilters;

namespace WorkoutGlobal.UI.Extensions
{
    /// <summary>
    /// Base class for all extension services.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure instances of attributes.
        /// </summary>
        /// <param name="services">Project services.</param>
        public static void ConfigureAttributes(this IServiceCollection services)
        {
            services.AddScoped<ModelValidationFilterAttribute>();
        }
    }
}
