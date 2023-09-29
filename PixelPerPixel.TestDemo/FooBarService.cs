using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Repositories.Interfaces;
using PixelPerPixel.TestDemo.Services.Interfaces;

namespace PixelPerPixel.TestDemo.Services
{
    public class FooBarService : IFooBarService
    {
        private readonly IFooBarRepository repository;

        public FooBarService(IFooBarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<FooBar> SaveFooBar(FooBar fooBar)
        {
            fooBar.Bar += "abc";

            return await this.repository.SaveFooBar(fooBar);
        }

        public async Task<FooBar> GetFooBar(int foo)
        {
            return await this.repository.GetFooBar(foo);
        }
    }
}