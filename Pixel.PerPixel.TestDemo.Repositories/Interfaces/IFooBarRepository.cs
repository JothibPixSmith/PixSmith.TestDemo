using PixelPerPixel.TestDemo.Domain;

namespace PixelPerPixel.TestDemo.Repositories.Interfaces;

public interface IFooBarRepository
{
    Task<FooBar> SaveFooBar(FooBar fooBar);

    Task<FooBar> GetFooBar(int foo);
}