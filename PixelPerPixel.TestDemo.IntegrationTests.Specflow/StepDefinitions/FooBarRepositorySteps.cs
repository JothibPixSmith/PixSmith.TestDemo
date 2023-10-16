using NUnit.Framework;
using PixelPerPixel.TestDemo.DbContext;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.IntegrationTests.Specflow.Fxitures;
using PixelPerPixel.TestDemo.Repositories;
using PixelPerPixel.TestDemo.Repositories.Interfaces;

namespace PixelPerPixel.TestDemo.IntegrationTests.Specflow.StepDefinitions;

[Binding]
public class FooBarRepositorySteps
{
    private readonly MongoDbContext dbContext;
    private IFooBarRepository fooBarRepository;
    private FooBar resultingFooBar;

    public FooBarRepositorySteps(MongoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [Given(@"FooBar Repository is initialized")]
    public void GivenFooBarRepositoryIsInitialized()
    {
        this.fooBarRepository = new FooBarRepository(this.dbContext);
    }

    [When(@"FooBar Repository SaveFooBar method is called")]
    public async Task WhenFooBarRepositorySaveFooBarMethodIsCalled()
    {
        this.resultingFooBar = await this.fooBarRepository.SaveFooBar(FooBarFixture.Default);
    }

    [Then(@"FooBar Repository returns FooBar with non-null id field")]
    public void ThenFooBarRepositoryReturnsFooBarWithNon_NullIdField()
    {
        Assert.IsNotNull(this.resultingFooBar.Id);
    }
}