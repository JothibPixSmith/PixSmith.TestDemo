using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PixelPerPixel.TestDemo.DbContext;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Domain.Models.Settings;
using PixelPerPixel.TestDemo.IntegrationTests.xUnit.Fixtures;
using PixelPerPixel.TestDemo.Repositories;
using PixelPerPixel.TestDemo.Repositories.Interfaces;

namespace PixelPerPixel.TestDemo.IntegrationTests.xUnit;

public class FooBarRepositoryTests : IAsyncDisposable
{
    private readonly IConfigurationRoot configuration;

    private readonly MongoDbContext dbContext;

    public FooBarRepositoryTests()
    {
        this.configuration = Configuration.Configuration.InitConfiguration();

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

    [Fact]
    public async Task SaveFooBarTest()
    {
        IFooBarRepository repository = new FooBarRepository(this.dbContext);

        var savedFooBar = await repository.SaveFooBar(FooBarFixture.Default);

        Assert.NotNull(savedFooBar.Id);
    }

    [Fact]
    public async Task GetFooBarTest()
    {
        await this.dbContext.FooBarCollection.InsertOneAsync(FooBarFixture.Default);

        IFooBarRepository repository = new FooBarRepository(this.dbContext);

        var savedFooBar = await repository.SaveFooBar(FooBarFixture.Default);

        Assert.NotNull(savedFooBar.Id);
    }

    public async ValueTask DisposeAsync()
    {
        await this.dbContext.FooBarCollection.DeleteManyAsync(FilterDefinition<FooBar>.Empty);

        GC.SuppressFinalize(this);
    }
}