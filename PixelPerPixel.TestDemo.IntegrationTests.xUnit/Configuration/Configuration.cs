using Microsoft.Extensions.Configuration;

namespace PixelPerPixel.TestDemo.IntegrationTests.xUnit.Configuration
{
    public static class Configuration
    {
        public static IConfigurationRoot InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}
