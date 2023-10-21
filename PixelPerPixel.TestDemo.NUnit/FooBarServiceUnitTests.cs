using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Services;
using PixelPerPixel.TestDemo.Services.Interfaces;
using PixelPerPixel.TestDemo.UnitTests.NUnit.Fixtures;
using PixelPerPixel.TestDemo.UnitTests.NUnit.Mocks;

namespace PixelPerPixel.TestDemo.UnitTests.NUnit
{
    public class FooBarServiceUnitTests
    {
        private FooBarRepositoryMocks repositoryMocks;

        private static IEnumerable<FooBar> fooBarDataDrivenTestData;

        [SetUp]
        public void Setup()
        {
            this.repositoryMocks = new FooBarRepositoryMocks();

            this.repositoryMocks.MockSaveFooBar();

            this.repositoryMocks.MockGetFooBar(FooBarFixture.Default);

            fooBarDataDrivenTestData = FooBarTestCaseFixtures.FooBarDataDrivenTestData().ToList();
        }

        [Test]
        public async Task SaveFooBarServiceTest()
        {
            IFooBarService service = new FooBarService(this.repositoryMocks.RepositoryMock.Object);

            var savedFooBar = await service.SaveFooBar(FooBarFixture.Default);

            Assert.True(savedFooBar.Bar.EndsWith("abc"));
        }

        [Test]
        [TestCaseSource(nameof(TestCaseFooBarFixture))]
        public async Task DataDrivenSaveFooBarServiceTest(FooBar fooBar)
        {
            IFooBarService service = new FooBarService(this.repositoryMocks.RepositoryMock.Object);

            var savedFooBar = await service.SaveFooBar(fooBar);

            Assert.True(savedFooBar.Bar.EndsWith("abc"));
        }

        [Test]
        public async Task GetFooBarServiceTest()
        {
            IFooBarService service = new FooBarService(this.repositoryMocks.RepositoryMock.Object);

            var savedFooBar = await service.GetFooBar(123);

            Assert.That(savedFooBar.Foo, Is.EqualTo(123));
        }

        private static IEnumerable<FooBar> TestCaseFooBarFixture()
        {

            yield return new FooBar
            {
                Foo = 1,
                Bar = "1"
            };

            yield return new FooBar
            {
                Foo = 2,
                Bar = "2"
            };

            yield return new FooBar
            {
                Foo = 3,
                Bar = "3"
            };
        }
    }
}