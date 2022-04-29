using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutGlobal.Monitoring.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutGlobal.Monitoring.Tests
{
    public class HeathChecksTests
    {
        [Fact]
        public void Status_200OK_AllHealthChecksAreHealthy()
        {
            // arrange
            var checks = new HealthReport(new Dictionary<string, HealthReportEntry>()
            {
                { "Check 1", new HealthReportEntry(HealthStatus.Healthy, "Description 1", TimeSpan.MinValue, null, null) },
                { "Check 2", new HealthReportEntry(HealthStatus.Healthy, "Description 2", TimeSpan.MinValue, null, null) },
                { "Check 3", new HealthReportEntry(HealthStatus.Healthy, "Description 3", TimeSpan.MinValue, null, null) },
                { "Check 4", new HealthReportEntry(HealthStatus.Healthy, "Description 4", TimeSpan.MinValue, null, null) },
            }, TimeSpan.MinValue);

            var controller = new HealthChecksController();

            // act
            var actionResult = controller.GetHealthChecksStatus(checks) as ViewResult;
            var model = actionResult?.Model;

            // assert
            actionResult.Should().NotBeNull();
            model.Should().NotBeNull();
        }
    }
}
