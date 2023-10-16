using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.RestService;
using PixelPerPixel.TestDemo.Services.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PixelPerPixel.TestDemo.IntegrationTests.Specflow.HttpContexts
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
                    .UseStartup<Startup>()
                    .UseConfiguration(configuration);
        }

        public HttpStatusCode HttpResponseForSaveFooBar { get; private set; }

        public HttpStatusCode HttpResponseForGetFooBar { get; private set; }

        public IFooBarService FooBarServiceReplacement { get; set; }

        private void StartApp()
        {
            webHostBuilder.ConfigureTestServices(services =>
            {
                if (FooBarServiceReplacement != null)
                {
                    var currentService = services
                        .FirstOrDefault(x =>
                            x.ServiceType == typeof(IFooBarService));

                    if (currentService != null)
                    {
                        services.Remove(currentService);

                        services.AddTransient(x => FooBarServiceReplacement);
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
            StartApp();

            var response
                = await this.clientForTests.PostAsync("api/foobar",
                    new StringContent(JsonSerializer.Serialize(fooBar), Encoding.UTF8, "application/json"));

            HttpResponseForSaveFooBar = response.StatusCode;

            return JsonSerializer.Deserialize<FooBar>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }

        public async Task<FooBar> GetFooBar(int foo)
        {
            StartApp();

            var response
                = await this.clientForTests.GetAsync($"api/foobar/{foo}");

            HttpResponseForGetFooBar = response.StatusCode;

            return JsonSerializer.Deserialize<FooBar>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}
