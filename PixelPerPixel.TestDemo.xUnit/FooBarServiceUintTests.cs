using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Services;
using PixelPerPixel.TestDemo.Services.Interfaces;
using PixelPerPixel.TestDemo.UnitTests.xUnit.Fixtures;
using PixelPerPixel.TestDemo.UnitTests.xUnit.Mocks;

namespace PixelPerPixel.TestDemo.UnitTests.xUnit
{
    public class FooBarServiceUintTests
    {
        private readonly FooBarRepositoryMocks repositoryMock;

        public FooBarServiceUintTests(FooBarRepositoryMocks repositoryMock)
        {
            this.repositoryMock = repositoryMock;

            this.repositoryMock.MockSaveFooBar();

            this.repositoryMock.MockGetFooBar(FooBarFixture.Default);
        }

        [Fact]
        public async Task SaveFooBarServiceTest()
        {
            IFooBarService service = new FooBarService(this.repositoryMock.RepositoryMock.Object);

            var savedFooBar = await service.SaveFooBar(FooBarFixture.Default);

            Assert.True(savedFooBar.Bar.EndsWith("abc"));
        }

        [Theory]
        [MemberData(nameof(FooBarTestData))]
        public async Task SaveFooBarServiceTestDataDriven(FooBar fooBar)
        {
            IFooBarService service = new FooBarService(this.repositoryMock.RepositoryMock.Object);

            var savedFooBar = await service.SaveFooBar(fooBar);

            Assert.True(savedFooBar.Bar.EndsWith("abc"));
        }

        [Fact]
        public async Task GetFooBarServiceTest()
        {
            IFooBarService service = new FooBarService(this.repositoryMock.RepositoryMock.Object);

            var savedFooBar = await service.GetFooBar(123);

            Assert.Equal(123, savedFooBar.Foo);
        }

        public static IEnumerable<object[]> FooBarTestData => new List<FooBar[]>
        {
            new FooBar[] { new FooBar { Foo = 1, Bar = "1" }},
            new FooBar[] { new FooBar { Foo = 2, Bar = "2" }},
            new FooBar[] { new FooBar { Foo = 3, Bar = "3" }}
        };
    }
}