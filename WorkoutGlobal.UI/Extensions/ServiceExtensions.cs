﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using WorkoutGlobal.UI.ApiConnection.Contracts;
using WorkoutGlobal.UI.ApiConnection.HttpClientHandlers;
using WorkoutGlobal.UI.ApiConnection.Services;
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

        /// <summary>
        /// Configure connection dependencies.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApiConnection(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<AuthenticationHttpClientHandler>();
        }
    }
}