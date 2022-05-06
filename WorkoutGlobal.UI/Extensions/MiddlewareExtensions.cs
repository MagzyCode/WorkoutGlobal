using WorkoutGlobal.UI.Middlewares;

namespace WorkoutGlobal.UI.Extensions
{
    /// <summary>
    /// Base class for all extension middlewares.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Add to request pipeline exception handler.
        /// </summary>
        /// <param name="builder">Instance of application builder.</param>
        public static void UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
