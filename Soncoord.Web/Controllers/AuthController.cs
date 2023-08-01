using Microsoft.AspNetCore.Mvc;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Database;

namespace Soncoord.Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        // WIP - Just a test endpoint
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/db")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> Test(IDatabaseService service)
        {
            return await service.GetData();
        }

        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/bot/twitch")]
        [HttpGet]
        public async Task<IActionResult> Authorize(ITwitchService twitchService)
        {
            return Redirect(await twitchService.Authorize());
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
            IDatabaseService database)
        {
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(errorDescription);
            }

            if (string.IsNullOrEmpty(state))
            {
                return BadRequest();
            }

            var login = await database.GetLoginAsync(state);
            if (state != login.State)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(code))
            {
                var result = await twitchService.GetTokenAsync(code);
                if (result is null)
                {
                    return BadRequest();
                }
                
                var validateResult = await twitchService.ValidateTokenAsync(result.AccessToken);
                if (validateResult?.Status is null)
                {
                    // Save Data
                    return Ok();
                }
                else
                {
                    return BadRequest(validateResult);
                }
            }

            return BadRequest();
        }
    }
}
