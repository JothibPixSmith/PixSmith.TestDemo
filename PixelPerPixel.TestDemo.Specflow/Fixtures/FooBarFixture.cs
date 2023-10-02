using PixelPerPixel.TestDemo.Domain;

namespace PixelPerPixel.TestDemo.UnitTests.Specflow.Fixtures;

public static class FooBarFixture
{
    public static FooBar Default => new FooBar
    {
        Foo = 123,
        Bar = "test"
    };
}