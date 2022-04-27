using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WorkoutGlobal.Monitoring.Controllers
{
    public class HealthChecksController : Controller
    {
        [HttpGet]
        public IActionResult Status(HealthReport healthReport)
        {
            var isHealthy = healthReport.Entries
                .All(x => x.Value.Status == HealthStatus.Healthy);

            var status = isHealthy 
                ? StatusCodes.Status200OK
                : StatusCodes.Status500InternalServerError;

            return View(status);
        }
    }
}
