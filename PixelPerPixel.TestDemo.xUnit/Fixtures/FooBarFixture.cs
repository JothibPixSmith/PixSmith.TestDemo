using PixelPerPixel.TestDemo.Domain;

namespace PixelPerPixel.TestDemo.Tests.xUnit.Fixtures;

public static class FooBarFixture
{
    public static FooBar Default => new FooBar
    {
        Foo = 123,
        Bar = "test"
    };
}