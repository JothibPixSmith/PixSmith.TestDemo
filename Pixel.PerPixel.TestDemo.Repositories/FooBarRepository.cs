using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PixelPerPixel.TestDemo.DbContext;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Repositories.Interfaces;

namespace PixelPerPixel.TestDemo.Repositories
{
    public class FooBarRepository : IFooBarRepository
    {
        private readonly MongoDbContext mongoDbContext;

        public FooBarRepository(MongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }

        public async Task<FooBar> SaveFooBar(FooBar fooBar)
        {
            var collection = this.mongoDbContext.FooBarCollection;

            var currentDocument = await collection.AsQueryable()
                .FirstOrDefaultAsync(x => x.Foo == fooBar.Foo);

            if (currentDocument != null)
            {
                fooBar.Id = currentDocument.Id;
            }

            await collection.ReplaceOneAsync(
                Builders<FooBar>.Filter.Eq(x => x.Id, fooBar.Id),
                 fooBar,
                new ReplaceOptions { IsUpsert = true });

            return fooBar;
        }

        public async Task<FooBar> GetFooBar(int foo)
        {
            var collection = this.mongoDbContext.FooBarCollection;

            return await collection.AsQueryable().SingleAsync(x => x.Foo == foo);
        }
    }
}