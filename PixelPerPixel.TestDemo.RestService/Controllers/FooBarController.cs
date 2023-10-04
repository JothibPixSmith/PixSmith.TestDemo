using Microsoft.AspNetCore.Mvc;
using PixelPerPixel.TestDemo.Domain;
using PixelPerPixel.TestDemo.Services.Interfaces;

namespace PixelPerPixel.TestDemo.RestService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FooBarController : Controller
    {
        private readonly IFooBarService fooBarService;

        public FooBarController(IFooBarService fooBarService)
        {
            this.fooBarService = fooBarService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<FooBar> SaveFooBar([FromBody] FooBar fooBar)
        {
            return await this.fooBarService.SaveFooBar(fooBar);
        }

        [HttpGet("{foo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<FooBar> GetFooBar([FromRoute] int foo)
        {
            return await this.fooBarService.GetFooBar(foo);
        }
    }
}
