using PixelPerPixel.TestDemo.Domain.Models.Settings;
using PixelPerPixel.TestDemo.Repositories;
using PixelPerPixel.TestDemo.Repositories.Interfaces;
using PixelPerPixel.TestDemo.Services;
using PixelPerPixel.TestDemo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IFooBarRepository, FooBarRepository>();

builder.Services.AddTransient<IFooBarService, FooBarService>();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
