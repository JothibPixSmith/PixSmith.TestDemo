using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PixelPerPixel.TestDemo.DbContext;
using PixelPerPixel.TestDemo.Domain.Models.Settings;
using PixelPerPixel.TestDemo.IntegrationTests.NUnit.Fixture;
using PixelPerPixel.TestDemo.Repositories;
using PixelPerPixel.TestDemo.Repositories.Interfaces;

namespace PixelPerPixel.TestDemo.IntegrationTests.NUnit;

public class FooBarRepositoryTests
{
    private MongoDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("PixelPerPixel.TestDemo.IntegrationTests.NUnit.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration[$"{nameof(MongoDbSettings)}:{nameof(MongoDbSettings.ConnectionString)}"];

        var databaseName = configuration[$"{nameof(MongoDbSettings)}:{nameof(MongoDbSettings.DatabaseName)}"];

        var collectionName = configuration[$"{nameof(MongoDbSettings)}:{nameof(MongoDbSettings.CollectionName)}"];

        this.dbContext = new MongoDbContext(Options.Create(new MongoDbSettings()
        {
            ConnectionString = connectionString,
            DatabaseName = databaseName,
            CollectionName = collectionName
        }));
    }

    [Test, Order(1)]
    public async Task SaveFooBarTest()
    {
        IFooBarRepository repository = new FooBarRepository(this.dbContext);

        var newFooBar = await repository.SaveFooBar(FooBarFixture.Default);

        Assert.IsNotNull(newFooBar.Id);
    }

    [Test, Order(2)]
    public async Task SaveFooBarTestNewEntity()
    {
        var fooBarToSave = FooBarFixture.Default;

        fooBarToSave.Foo = 12;

        IFooBarRepository repository = new FooBarRepository(this.dbContext);

        var newFooBar = await repository.SaveFooBar(fooBarToSave);

        Assert.IsNotNull(newFooBar.Id);
    }

    [Test, Order(3)]
    public async Task GetFooBarTest()
    {
        IFooBarRepository repository = new FooBarRepository(this.dbContext);

        var newFooBar = await repository.GetFooBar(123);

        Assert.IsNotNull(newFooBar.Id);
    }
}