using BoDi;
using Microsoft.Extensions.Configuration;

namespace PixelPerPixel.TestDemo.IntegrationTests.Specflow.Configuration
{
    [Binding]
    public class ConfigurationSteps
    {
        private readonly IObjectContainer objectContainer;

        public ConfigurationSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [Given(@"Configuration is setup up")]
        public void GivenConfigurationIsSetupUp()
        {
            this.objectContainer.RegisterInstanceAs(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build());
        }
    }
}
