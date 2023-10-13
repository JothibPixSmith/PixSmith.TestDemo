using PixelPerPixel.TestDemo.IntegrationTests.NUnit.Fixture;
using PixelPerPixel.TestDemo.IntegrationTests.NUnit.Mocks;

namespace PixelPerPixel.TestDemo.IntegrationTests.NUnit
{
    public class FooBarControllerTests
    {
        private HttpContexts.RestService restService;

        private FooBarServiceMock serviceMock;

        [SetUp]
        public void Setup()
        {
            this.restService = new HttpContexts.RestService();

            this.serviceMock = new FooBarServiceMock();
        }

        [Test]
        public async Task FooBarControllerSaveFooBarTest()
        {
            this.serviceMock.MockSaveFooBar();

            this.restService.FooBarServiceReplacement = this.serviceMock.ServiceMock.Object;

            var savedFooBar = await restService.SaveFooBar(FooBarFixture.Default);

            Assert.That(savedFooBar.Foo, Is.EqualTo(FooBarFixture.Default.Foo));
        }

        [Test]
        public async Task FooBarControllerGetFooBarTest()
        {
            this.serviceMock.MockGetFooBar(FooBarFixture.Default);

            this.restService.FooBarServiceReplacement = this.serviceMock.ServiceMock.Object;

            var savedFooBar = await restService.GetFooBar(123);

            Assert.That(savedFooBar.Foo, Is.EqualTo(FooBarFixture.Default.Foo));
        }
    }
}