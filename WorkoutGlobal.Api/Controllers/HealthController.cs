﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IHealthRepository _healthRepository;
        public HealthController(IHealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }

        // TODO: Дополнить описанием
        [HttpGet("connection")]
        [ProducesResponseType(type: typeof(HealthStatus), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckDbConnection()
        {
            var connectionState = await _healthRepository.CanConnectAsync();

            return Ok(new { HealthStatus = connectionState.Status.ToString() });
        }

        [HttpGet("ping")]
        [ProducesResponseType(type: typeof(HealthStatus), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckApiAlive()
        {
            var connectionState = await _healthRepository.CanConnectAsync();

            return Ok(new { HealthStatus = connectionState.Status.ToString() });
        }

    }
}
