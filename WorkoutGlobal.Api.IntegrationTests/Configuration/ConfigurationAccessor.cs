using Microsoft.Extensions.Configuration;
using System.IO;

namespace WorkoutGlobal.Api.IntegrationTests.Configuration
{
    public static class ConfigurationAccessor
    {
        public static IConfiguration GetTestConfiguration(string settingFilePath = "appsettings.json")
        {
            var dir = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingFilePath, optional: false, reloadOnChange: true)
                .Build();
            return configuration;
        }
    }
}
