using PixelPerPixel.TestDemo.RestService;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(
            (hostingContext, configurationBuilder) =>
            {
                configurationBuilder.AddJsonFile("appsettings.json");
            })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}
