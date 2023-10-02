using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Services;
using PixelPerPixel.TestDemo.Services.Interfaces;
using PixelPerPixel.TestDemo.UnitTests.Specflow.Fixtures;
using PixelPerPixel.TestDemo.UnitTests.Specflow.Mocks;

namespace PixelPerPixel.TestDemo.UnitTests.Specflow.StepDefinitions
{
    [Binding]
    public sealed class FooBarServiceSteps
    {
        private readonly FooBarRepositoryMocks repositoryMock;
        private FooBar expected;
        private FooBar result;

        public FooBarServiceSteps(FooBarRepositoryMocks repositoryMock)
        {
            this.repositoryMock = repositoryMock;
        }

        [Given(@"A Default Foobar instance is created")]
        public void GivenADefaultFoobarInstanceIsCreated()
        {
            expected = FooBarFixture.Default;
        }

        [Given(@"the FooBarRepository get method is mocked")]
        public void GivenTheFooBarRepositoryIsMocked()
        {
            this.repositoryMock.MockGetFooBar(expected);
        }

        [Given(@"the FooBarRepository save method is mocked")]
        public void GivenTheFooBarRepositorySaveMethodIsMocked()
        {
            this.repositoryMock.MockSaveFooBar();
        }

        [When(@"SaveFooBar is called")]
        public async Task WhenSaveFooBarIsCalled()
        {
            IFooBarService service = new FooBarService(this.repositoryMock.RepositoryMock.Object);

            this.result = await service.SaveFooBar(FooBarFixture.Default);
        }

        [When(@"GetFooBar is called")]
        public async Task WhenGetFooBarIsCalled()
        {
            IFooBarService service = new FooBarService(this.repositoryMock.RepositoryMock.Object);

            this.result = await service.GetFooBar(123);
        }

        [Then(@"the resulting FooBar instance bar property must end with '([^']*)'")]
        public void ThenTheResultingFooBarInstanceBarPropertyMustEndWith(string suffixToTest)
        {
            Assert.Equal($"{expected.Bar}{suffixToTest}", result.Bar);
        }

        [Then(@"the resulting FooBar instance is not null")]
        public void ThenTheResultingFooBarInstanceNotNull()
        {
            Assert.NotNull(result);
        }
    }
}