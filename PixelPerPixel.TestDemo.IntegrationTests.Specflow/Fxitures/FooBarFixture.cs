using PixelPerPixel.TestDemo.Domain;

namespace PixelPerPixel.TestDemo.IntegrationTests.Specflow.Fxitures;

public static class FooBarFixture
{
    public static FooBar Default => new FooBar
    {
        Foo = 123,
        Bar = "test"
    };
}