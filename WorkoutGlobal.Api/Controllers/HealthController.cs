using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    /// <summary>
    /// Represents health controller.
    /// </summary>
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Health repository instance.
        /// </summary>
        private readonly IHealthRepository _healthRepository;

        /// <summary>
        /// Ctor for health controller.
        /// </summary>
        /// <param name="healthRepository">Health repository instance.</param>
        public HealthController(IHealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }

        /// <summary>
        /// Checks if connection with database is created.
        /// </summary>
        /// <response code="200">Get health status of database connection.</response>
        /// <response code="500">Something going wrong on server.</response>
        [HttpGet("connection")]
        [ProducesResponseType(type: typeof(HealthStatus), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckDbConnection()
        {
            var connectionState = await _healthRepository.CanConnectAsync();

            return Ok(new { HealthStatus = connectionState.Status.ToString() });
        }

        /// <summary>
        /// Checks if API working.
        /// </summary>
        /// <response code="200">Get health status of API work.</response>
        /// <response code="500">Something going wrong on server.</response>
        [HttpGet("ping")]
        [ProducesResponseType(type: typeof(string), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult CheckApiAlive()
        {
            return Ok(new { HealthStatus = HealthCheckResult.Healthy() });
        }

    }
}
