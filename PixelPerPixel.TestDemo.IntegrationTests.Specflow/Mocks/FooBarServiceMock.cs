using Moq;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Services.Interfaces;

namespace PixelPerPixel.TestDemo.IntegrationTests.Specflow.Mocks
{
    public class FooBarServiceMock
    {
        public FooBarServiceMock()
        {
            ServiceMock = new Mock<IFooBarService>();
        }

        public Mock<IFooBarService> ServiceMock { get; private set; }

        public void MockSaveFooBar(FooBar fooBar = null)
        {
            if (fooBar == null)
            {
                ServiceMock.Setup(x => x.SaveFooBar(It.IsAny<FooBar>()))
                    .ReturnsAsync((FooBar x) => x);

                return;
            }

            ServiceMock.Setup(x =>
                    x.SaveFooBar(It.Is<FooBar>(x => x.Foo == fooBar.Foo && x.Bar.StartsWith(fooBar.Bar))))
                .ReturnsAsync(fooBar);
        }

        public void MockGetFooBar(FooBar fooBar)
        {
            ServiceMock.Setup(x => x.GetFooBar(It.IsAny<int>()))
                .ReturnsAsync(fooBar);
        }
    }
}
