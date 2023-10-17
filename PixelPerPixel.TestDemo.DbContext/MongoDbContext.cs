using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Domain.Models.Settings;

namespace PixelPerPixel.TestDemo.DbContext
{
    public class MongoDbContext
    {
        private readonly string? databaseName;

        private readonly string? collectionName;
        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            this.DbContext = new MongoClient(settings.Value.ConnectionString);

            this.databaseName = settings.Value.DatabaseName;

            this.collectionName = settings.Value.CollectionName;
            if (!BsonClassMap.IsClassMapRegistered(typeof(DomainBase)))
            {
                BsonClassMap.TryRegisterClassMap<DomainBase>(x =>
                {
                    x.AutoMap();
                    x.MapIdMember(y => y.Id)
                        .SetIdGenerator(new StringObjectIdGenerator())
                        .SetSerializer(new StringSerializer(BsonType.ObjectId)); ;
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(FooBar)))
            {
                BsonClassMap.TryRegisterClassMap<FooBar>(x =>
                {
                    x.AutoMap();
                });
            }

        }

        public MongoClient DbContext { get; private set; }

        public IMongoDatabase FooBarDatabase => DbContext.GetDatabase(this.databaseName ?? nameof(FooBar));

        public IMongoCollection<FooBar> FooBarCollection => FooBarDatabase.GetCollection<FooBar>(this.collectionName ?? nameof(FooBar));

    }
}