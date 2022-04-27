using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutGlobal.Monitoring.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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

            // act & assert
            var actionResult = controller.Status(checks) as ViewResult ;

            actionResult.Should().NotBeNull();

            var model = actionResult!.Model as int?;

            model.Should().NotBeNull()
                .And.BeOfType(typeof(int))
                .And.Be(StatusCodes.Status200OK);
        }

    }
}
