using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.HealthRepository
{
    /// <summary>
    /// Represents health repository class.
    /// </summary>
    public class HealthRepository : BaseConnection, IHealthRepository
    {
        /// <summary>
        /// Sets database context and project configuration.
        /// </summary>
        /// <param name="workoutGlobalContext">Database context.</param>
        /// <param name="configurationManager">Project configuration.</param>
        public HealthRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        { }

        /// <summary>
        /// Check if database connection is open.
        /// </summary>
        /// <returns>Health check result.</returns>
        public async Task<HealthCheckResult> CanConnectAsync()
        {
            var isConnected = await Context.Database.CanConnectAsync();
            return isConnected
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();
        }

        /// <summary>
        /// Check if api alive.
        /// </summary>
        /// <returns>Health check result.</returns>
        public async Task<HealthCheckResult> IsApiAlive()
        {
            using var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Configuration.GetSection("AplicationUrl").Value)
            };

            var response = await httpClient.GetAsync("api/health/ping");

            return (response != null && response.StatusCode == HttpStatusCode.OK)
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();
        }
    }
}
