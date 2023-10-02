using Microsoft.Extensions.DependencyInjection;
using PixelPerPixel.TestDemo.UnitTests.xUnit.Mocks;

namespace PixelPerPixel.TestDemo.UnitTests.xUnit
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FooBarRepositoryMocks>();
        }
    }
}
