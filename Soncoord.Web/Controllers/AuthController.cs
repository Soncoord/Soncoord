using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Soncoord.Business.Services.Database;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Configuration;

namespace Soncoord.Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        // WIP - Just a test endpoint
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/db")]
        [HttpGet]
        public async Task<IActionResult> Test(DatabaseService service)
        {
            var temp = await service.GetData();
            Console.WriteLine(temp);
            return null;
        }

        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/bot/twitch")]
        [HttpGet]
        public IActionResult Authorize(ITwitchService twitchService)
        {
            return Redirect(twitchService.Authorize());
        }

        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/bot/twitch/callback")]
        [HttpGet]
        public async Task<IActionResult> GetCode(
            [FromQuery] string? code,
            [FromQuery] string? state,
            [FromQuery] string? error,
            [FromQuery] string? errorDescription,
            ITwitchService twitchService,
            IOptions<AppSettings> options)
        {
            if (state != options.Value.Providers.Twitch.State)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(errorDescription);
            }

            if (!string.IsNullOrEmpty(code))
            { 
                return Ok(await twitchService.GetTokenAsync(code));
            }

            return BadRequest();
        }
    }
}
