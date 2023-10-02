using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace PixelPerPixel.TestDemo.IntegrationTests.NUnit.HttpContexts
{
    public class RestService
    {
        private readonly IWebHostBuilder webHostBuilder;

        private readonly IConfiguration configuration;

        private HttpClient clientForTests;

        public RestService()
        {
            this.configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            webHostBuilder = new WebHostBuilder()
                .UseConfiguration(configuration);
        }

        public HttpStatusCode HttpResponseForSaveFooBar { get; private set; }

        public HttpStatusCode HttpResponseForGetFooBar { get; private set; }

        public IFooBarService FooBarServiceReplacement { get; set; }

        private void StartApp()
        {
            webHostBuilder.ConfigureTestServices(services =>
            {
                if (this.FooBarServiceReplacement != null)
                {
                    var currentService = services
                        .FirstOrDefault(x =>
                            x.ServiceType == this.FooBarServiceReplacement.GetType());

                    if (currentService != null)
                    {
                        services.Remove(currentService);
                    }

                }
            });

            if (this.clientForTests == null)
            {
                var server = new TestServer(webHostBuilder);

                this.clientForTests = server.CreateClient();
            }
        }

        public async Task<FooBar> SaveFooBar(FooBar fooBar)
        {
            this.StartApp();

            var response
                = await this.clientForTests.PostAsync("foobar/savefoobar",
                    new StringContent(JsonSerializer.Serialize(fooBar)));

            this.HttpResponseForSaveFooBar = response.StatusCode;

            return JsonSerializer.Deserialize<FooBar>(await response.Content.ReadAsStringAsync());
        }

        public async Task<FooBar> GetFooBar(int foo)
        {
            this.StartApp();

            var response
                = await this.clientForTests.GetAsync($"foobar/get/{foo}");

            this.HttpResponseForGetFooBar = response.StatusCode;

            return JsonSerializer.Deserialize<FooBar>(await response.Content.ReadAsStringAsync());
        }
    }
}
