using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;

namespace WorkoutGlobal.Api.HealthChecks
{
    /// <summary>
    /// Health check for database connection work.
    /// </summary>
    public class DatabaseConnectionHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Repository manager instance.
        /// </summary>
        private readonly IRepositoryManager _repositoryManager;

        /// <summary>
        /// Sets repositoty manager instance.
        /// </summary>
        /// <param name="repositoryManager">Repository manager instance.</param>
        public DatabaseConnectionHealthCheck(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        /// <summary>
        /// Check health of database connection.
        /// </summary>
        /// <param name="context">Health check context.</param>
        /// <param name="cancellationToken">Notitification that operations should be canceled.</param>
        /// <returns>Health check result.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var connectionState = await _repositoryManager.HealthRepository.CanConnectAsync();

            return connectionState;
        }
    }
}
