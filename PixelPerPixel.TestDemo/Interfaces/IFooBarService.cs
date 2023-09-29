using PixelPerPixel.TestDemo.Domain;

namespace PixelPerPixel.TestDemo.Services.Interfaces;

public interface IFooBarService
{
    Task<FooBar> SaveFooBar(FooBar foobar);

    Task<FooBar> GetFooBar(int foo);
}