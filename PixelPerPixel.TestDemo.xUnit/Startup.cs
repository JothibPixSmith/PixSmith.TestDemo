using Microsoft.Extensions.DependencyInjection;
using PixelPerPixel.TestDemo.Tests.xUnit.Mocks;

namespace PixelPerPixel.TestDemo.Tests.xUnit
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FooBarRepositoryMocks>();
        }
    }
}
