using BoDi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PixelPerPixel.TestDemo.DbContext;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Domain.Models.Settings;

namespace PixelPerPixel.TestDemo.IntegrationTests.Specflow.Database
{
    [Binding]
    public class DatabaseSteps
    {
        private readonly IObjectContainer objectContainer;
        private readonly IConfigurationRoot configuration;
        private MongoDbContext dbContext;

        public DatabaseSteps(IObjectContainer objectContainer, IConfigurationRoot configuration)
        {
            this.objectContainer = objectContainer;
            this.configuration = configuration;


        }
        [Given(@"Database is registered")]
        public void GivenDatabaseIsRegistered()
        {
            var connectionString = configuration[$"{nameof(MongoDbSettings)}:{nameof(MongoDbSettings.ConnectionString)}"];

            var databaseName = configuration[$"{nameof(MongoDbSettings)}:{nameof(MongoDbSettings.DatabaseName)}"];

            var collectionName = configuration[$"{nameof(MongoDbSettings)}:{nameof(MongoDbSettings.CollectionName)}"];

            this.dbContext = new MongoDbContext(Options.Create(new MongoDbSettings()
            {
                ConnectionString = connectionString,
                DatabaseName = databaseName,
                CollectionName = collectionName
            }));

            this.objectContainer.RegisterInstanceAs(this.dbContext);
        }

        [AfterScenario("AfterScenario.Database.CleanDatabase")]
        public async Task CleanupDatabase()
        {
            await this.dbContext.FooBarCollection.DeleteManyAsync(FilterDefinition<FooBar>.Empty);
        }

    }
}
