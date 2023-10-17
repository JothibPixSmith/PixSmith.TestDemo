using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.IntegrationTests.xUnit.Fixtures;
using PixelPerPixel.TestDemo.IntegrationTests.xUnit.MockExtensions;
using PixelPerPixel.TestDemo.IntegrationTests.xUnit.WebFactories;
using System.Text;
using System.Text.Json;

namespace PixelPerPixel.TestDemo.IntegrationTests.xUnit
{
    public class FooBarControllerTests : IClassFixture<IntegrationTestWebFactory<Program>>
    {
        private readonly HttpClient client;
        private readonly IntegrationTestWebFactory<Program> factory;

        public FooBarControllerTests(IntegrationTestWebFactory<Program> factory)
        {
            this.factory = factory;
            this.client = this.factory.CreateClient();
        }

        [Fact]
        public async Task SaveFooBarTest()
        {
            this.factory.FooBarServiceMock.MockSaveFooBar();

            var response = await this.client.PostAsync("api/foobar",
                new StringContent(JsonSerializer.Serialize(FooBarFixture.Default), Encoding.UTF8, "application/json"));

            var savedFooBar = await JsonSerializer.DeserializeAsync<FooBar>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

            Assert.NotNull(savedFooBar);
        }

        [Fact]
        public async Task GetFooBarTest()
        {
            this.factory.FooBarServiceMock.MockGetFooBar(FooBarFixture.Default);

            var response
                = await this.client.GetAsync($"api/foobar/{123}");

            var savedFooBar = await JsonSerializer.DeserializeAsync<FooBar>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

            Assert.NotNull(savedFooBar);
        }
    }
}