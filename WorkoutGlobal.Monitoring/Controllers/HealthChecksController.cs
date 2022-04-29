using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WorkoutGlobal.Monitoring.Controllers
{
    public class HealthChecksController : Controller
    {
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
