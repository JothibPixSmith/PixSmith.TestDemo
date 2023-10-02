using Moq;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Repositories.Interfaces;

namespace PixelPerPixel.TestDemo.UnitTests.Specflow.Mocks;

public class FooBarRepositoryMocks
{
    public Mock<IFooBarRepository> RepositoryMock { get; private set; } = new();

    public void MockSaveFooBar(FooBar fooBar = null)
    {
        if (fooBar == null)
        {
            RepositoryMock.Setup(x => x.SaveFooBar(It.IsAny<FooBar>()))
                .ReturnsAsync((FooBar x) => x);

            return;
        }

        RepositoryMock.Setup(x =>
                x.SaveFooBar(It.Is<FooBar>(x => x.Foo == fooBar.Foo && x.Bar.StartsWith(fooBar.Bar))))
            .ReturnsAsync(fooBar);

    }

    public void MockGetFooBar(FooBar fooBar)
    {
        RepositoryMock.Setup(x => x.GetFooBar(It.IsAny<int>()))
            .ReturnsAsync(fooBar);

    }
}