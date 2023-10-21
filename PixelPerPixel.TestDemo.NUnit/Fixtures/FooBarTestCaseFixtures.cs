using PixelPerPixel.TestDemo.Domain;

namespace PixelPerPixel.TestDemo.UnitTests.NUnit.Fixtures
{
    public static class FooBarTestCaseFixtures
    {
        public static FooBar[] FooBarDataDrivenTestData()
        {
            return new[]
            {
                new FooBar
                {
                    Foo = 1,
                    Bar = "1"
                },

                new FooBar
                {
                    Foo = 2,
                    Bar = "2"
                },

                new FooBar
                {
                    Foo = 3,
                    Bar = "3"
                }
            };

        }
    }
}
