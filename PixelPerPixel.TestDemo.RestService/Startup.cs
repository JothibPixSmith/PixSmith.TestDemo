using PixelPerPixel.TestDemo.DbContext;
using PixelPerPixel.TestDemo.Domain.Models.Settings;
using PixelPerPixel.TestDemo.Repositories;
using PixelPerPixel.TestDemo.Repositories.Interfaces;
using PixelPerPixel.TestDemo.Services;
using PixelPerPixel.TestDemo.Services.Interfaces;

namespace PixelPerPixel.TestDemo.RestService
{
    public class Startup
    {
        public IConfiguration Configuration
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen();

            services.AddTransient<MongoDbContext>();

            services.AddTransient<IFooBarRepository, FooBarRepository>();

            services.AddTransient<IFooBarService, FooBarService>();

            services.Configure<MongoDbSettings>(
                Configuration.GetSection(nameof(MongoDbSettings)));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                }); ;
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
