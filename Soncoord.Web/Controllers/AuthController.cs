using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Configuration;
using Soncoord.Infrastructure.Database;

namespace Soncoord.Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
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
            IDatabaseService database,
            IOptions<AppSettings> options)
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
                var authResult = await twitchService.GetTokenAsync(code);
                if (authResult is null)
                {
                    return BadRequest();
                }
                
                var validateResult = await twitchService.ValidateTokenAsync(authResult.AccessToken);

                if (validateResult!.UserId != options.Value.Providers.Twitch.BotId)
                {
                    return BadRequest();
                }

                if (validateResult?.Status is null)
                {
                    await database.SaveBotDataAsync(new Bots
                    {
                        Name = validateResult!.Login!,
                        UserId = validateResult!.UserId!,
                        AccessToken = authResult.AccessToken,
                        RefreshToken = authResult.RefreshToken
                    });

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
