using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;

namespace WorkoutGlobal.Api.HealthChecks
{
    public class DatabaseConnectionHealthCheck : IHealthCheck
    {
        private readonly IRepositoryManager _repositoryManager;

        public DatabaseConnectionHealthCheck(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var connectionState = await _repositoryManager.HealthRepository.CanConnectAsync();

            return connectionState;
        }
    }
}
