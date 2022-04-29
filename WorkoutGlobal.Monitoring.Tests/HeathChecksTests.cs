using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutGlobal.Monitoring.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace WorkoutGlobal.Monitoring.Tests
{
    /// <summary>
    /// Represents test class for health checks controller.
    /// </summary>
    public class HeathChecksTests
    {
        /// <summary>
        /// Tests that health check return a 200 responeceand correct list of errors,
        /// if they exists, when health checks are healthy.
        /// </summary>
        [Fact]
        public void Status_200OK_AllHealthChecksAreHealthy()
        {
            // arrange
            var checks = new HealthReport(new Dictionary<string, HealthReportEntry>()
            {
                { "Check 1", new HealthReportEntry(HealthStatus.Healthy, "Description 1", TimeSpan.MinValue, null, null) },
                { "Check 2", new HealthReportEntry(HealthStatus.Healthy, "Description 2", TimeSpan.MinValue, null, null) },
            }, TimeSpan.MinValue);

            var controller = new HealthChecksController();

            // act
            var actionResult = controller.GetHealthChecksStatus(checks) as ViewResult;
            var model = JObject.Parse(
                JsonConvert.SerializeObject(actionResult?.Model));

            // assert
            actionResult.Should().NotBeNull();

            model.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.BeOfType<JObject>();

            model["StatusCode"].Value<int>()
                .Should().BeOfType(typeof(int))
                .And.Be(StatusCodes.Status200OK);

            model["Errors"].ToObject(typeof(List<string>))
                .Should().NotBeNull()
                .And.BeOfType<List<string>>()
                .And.BeAssignableTo<List<string>>().Which.Should()
                    .HaveCount(2);
        }
    }
}
