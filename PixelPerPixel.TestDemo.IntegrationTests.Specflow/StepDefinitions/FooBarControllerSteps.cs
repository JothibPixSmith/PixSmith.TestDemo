using NUnit.Framework;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.IntegrationTests.Specflow.Fxitures;
using PixelPerPixel.TestDemo.IntegrationTests.Specflow.Mocks;

namespace PixelPerPixel.TestDemo.IntegrationTests.Specflow.StepDefinitions
{
    [Binding]
    public sealed class FooBarControllerSteps
    {
        private Exception resultingException;

        private FooBar resultingFooBar;

        private readonly HttpContexts.RestService restService;

        private readonly FooBarServiceMock fooBarServiceMock;

        public FooBarControllerSteps(HttpContexts.RestService restService)
        {
            this.restService = restService;
            this.fooBarServiceMock = new FooBarServiceMock();
        }

        [Given(@"FooBarService SaveBoor Method is Mocked")]
        public void GivenFooBarServiceIsMocked()
        {
            fooBarServiceMock.MockSaveFooBar();

            restService.FooBarServiceReplacement = fooBarServiceMock.ServiceMock.Object;
        }

        [When(@"FooBar SaveFooBar is called")]
        public async Task WhenFooBarSaveFooBarIsCalled()
        {
            try
            {
                this.resultingFooBar = await this.restService.SaveFooBar(FooBarFixture.Default);
            }
            catch (Exception ex)
            {
                resultingException = ex;
            }
        }

        [Then(@"An instance of FooBar is returned")]
        public void ThenAnInstanceOfFooBarIsReturnedWithANonEmptyIdField()
        {
            Assert.IsNotNull(this.resultingFooBar);
        }

        [Given(@"FooBarService GetFooBar Method is Mocked")]
        public void GivenFooBarServiceGetFooBarMethodIsMocked()
        {
            fooBarServiceMock.MockGetFooBar(FooBarFixture.Default);

            restService.FooBarServiceReplacement = fooBarServiceMock.ServiceMock.Object;
        }

        [When(@"FooBar GetFooBar is called")]
        public async Task WhenFooBarGetFooBarIsCalled()
        {
            try
            {
                this.resultingFooBar = await this.restService.GetFooBar(123);
            }
            catch (Exception ex)
            {
                resultingException = ex;
            }
        }

    }
}