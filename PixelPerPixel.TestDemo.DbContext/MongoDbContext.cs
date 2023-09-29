using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Domain.Models.Settings;

namespace PixelPerPixel.TestDemo.DbContext
{
    public class MongoDbContext
    {

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            DbContext = new MongoClient(settings.Value.ConnectionString);
        }

        public MongoClient DbContext { get; private set; }

        public IMongoDatabase FooBarDatabase => DbContext.GetDatabase(nameof(FooBar));

        public IMongoCollection<FooBar> FooBarCollection => FooBarDatabase.GetCollection<FooBar>(nameof(FooBar));

    }
}