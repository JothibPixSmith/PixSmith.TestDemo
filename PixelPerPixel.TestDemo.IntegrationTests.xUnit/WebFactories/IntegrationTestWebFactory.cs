using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PixelPerPixel.TestDemo.Services.Interfaces;

namespace PixelPerPixel.TestDemo.IntegrationTests.xUnit.WebFactories
{
    public class IntegrationTestWebFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<IFooBarService> FooBarServiceMock { get; private set; }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var currentService = services
                    .FirstOrDefault(x =>
                        x.ServiceType == typeof(IFooBarService));

                if (currentService != null)
                {
                    services.Remove(currentService);

                    this.FooBarServiceMock = new Mock<IFooBarService>();

                    services.AddTransient(x => this.FooBarServiceMock.Object);
                }
            });
        }
    }
}
