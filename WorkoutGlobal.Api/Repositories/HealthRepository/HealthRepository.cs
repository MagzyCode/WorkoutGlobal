using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.DatabaseContext;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.HealthRepository
{
    public class HealthRepository : BaseConnection, IHealthRepository
    {
        public HealthRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager) 
            : base(workoutGlobalContext, configurationManager)
        { }

        public async Task<HealthCheckResult> CanConnectAsync()
        {
            var isConnected = await Context.Database.CanConnectAsync();
            return isConnected
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();
        }

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
