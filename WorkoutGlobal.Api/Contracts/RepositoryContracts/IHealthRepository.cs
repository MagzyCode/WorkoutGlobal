using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WorkoutGlobal.Api.Contracts
{
    /// <summary>
    /// Interface for base base methods of health repository.
    /// </summary>
    public interface IHealthRepository
    {
        /// <summary>
        /// Check if connection with database is open.
        /// </summary>
        /// <returns>Health status of database connection.</returns>
        public Task<HealthCheckResult> CanConnectAsync();

        /// <summary>
        /// Check if this project work.
        /// </summary>
        /// <returns>Health status of api work.</returns>
        public Task<HealthCheckResult> IsApiAlive();
    }
}
