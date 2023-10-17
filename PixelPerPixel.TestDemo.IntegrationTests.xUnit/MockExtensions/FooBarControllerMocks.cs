using Moq;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Services.Interfaces;

namespace PixelPerPixel.TestDemo.IntegrationTests.xUnit.MockExtensions
{
    public static class FooBarControllerMocks
    {
        public static void MockSaveFooBar(this Mock<IFooBarService> fooBarServiceMock, FooBar fooBar = null)
        {
            if (fooBar == null)
            {
                fooBarServiceMock.Setup(x => x.SaveFooBar(It.IsAny<FooBar>()))
                    .ReturnsAsync((FooBar x) => x);

                return;
            }

            fooBarServiceMock.Setup(x =>
                    x.SaveFooBar(It.Is<FooBar>(x => x.Foo == fooBar.Foo && x.Bar.StartsWith(fooBar.Bar))))
                .ReturnsAsync(fooBar);
        }

        public static void MockGetFooBar(this Mock<IFooBarService> fooBarServiceMock, FooBar fooBar)
        {
            fooBarServiceMock.Setup(x => x.GetFooBar(It.IsAny<int>()))
                .ReturnsAsync(fooBar);
        }
    }
}
