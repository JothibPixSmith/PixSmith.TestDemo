using PixelPerPixel.TestDemo.Services;
using PixelPerPixel.TestDemo.Services.Interfaces;
using PixelPerPixel.TestDemo.Tests.NUnit.Fixtures;
using PixelPerPixel.TestDemo.Tests.NUnit.Mocks;

namespace PixelPerPixel.TestDemo.Tests.NUnit
{
    public class FooBarServiceUnitTests
    {
        private FooBarRepositoryMocks repositoryMocks;

        [SetUp]
        public void Setup()
        {
            this.repositoryMocks = new FooBarRepositoryMocks();

            this.repositoryMocks.MockSaveFooBar();

            this.repositoryMocks.MockGetFooBar(FooBarFixture.Default);
        }

        [Test]
        public async Task SaveFooBarServiceTest()
        {
            IFooBarService service = new FooBarService(this.repositoryMocks.RepositoryMock.Object);

            var savedFooBar = await service.SaveFooBar(FooBarFixture.Default);

            Assert.True(savedFooBar.Bar.EndsWith("abc"));
        }

        [Test]
        public async Task GetFooBarServiceTest()
        {
            IFooBarService service = new FooBarService(this.repositoryMocks.RepositoryMock.Object);

            var savedFooBar = await service.GetFooBar(123);

            Assert.That(savedFooBar.Foo, Is.EqualTo(123));
        }
    }
}