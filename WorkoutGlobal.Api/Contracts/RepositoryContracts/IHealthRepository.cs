using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    public interface IHealthRepository
    {
        public Task<HealthCheckResult> CanConnectAsync();
        public Task<HealthCheckResult> IsApiAlive();
    }
}
