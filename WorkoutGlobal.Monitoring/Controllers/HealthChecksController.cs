using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WorkoutGlobal.Monitoring.Controllers
{
    /// <summary>
    /// Health check controller.
    /// </summary>
    public class HealthChecksController : Controller
    {
        /// <summary>
        /// Get status of given health checks.
        /// </summary>
        /// <param name="healthReport">Health checks report.</param>
        /// <returns>Status code of health check and list of errors.</returns>
        /// <exception cref="Exception">Throw when health checks report status is unvalid.</exception>
        [HttpGet]
        public IActionResult GetHealthChecksStatus(HealthReport healthReport)
        {
            var checksHealthDescription = new List<string>();

            foreach (var check in healthReport.Entries)
                checksHealthDescription.Add($"Check '{check.Key} had status {check.Value.Status}'");

            return View(new
            {
                StatusCode = healthReport.Status switch
                {
                    HealthStatus.Healthy => StatusCodes.Status200OK,
                    HealthStatus.Degraded => StatusCodes.Status200OK,
                    HealthStatus.Unhealthy => StatusCodes.Status503ServiceUnavailable,
                    _ => throw new Exception("Uncorrect type of health report status")
                },
                Errors = checksHealthDescription
            });
        }
    }
}
