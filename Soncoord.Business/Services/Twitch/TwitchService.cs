using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Auth;
using Soncoord.Infrastructure.Configuration;
using System.Web;

namespace Soncoord.Business.Services.Twitch
{
    public class TwitchService : ITwitchService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _options;

        public TwitchService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public string Authorize()
        {
            _options.Providers.Twitch.State = Guid.NewGuid().ToString();

            var queries = HttpUtility.ParseQueryString(string.Empty);
            queries.Add("response_type", "code");
            queries.Add("scope", "user:read:email");
            queries.Add("client_id", _options.Providers.Twitch.ClientId);
            queries.Add("client_secret", _options.Providers.Twitch.ClienbtSecret);
            queries.Add("redirect_uri", _options.Providers.Twitch.Callbacks.Bot);
            queries.Add("state", _options.Providers.Twitch.State);

            return $"{_options.Providers.Twitch.Endpoints.Authorize}?{queries}";
        }

        public async Task<TwitchResponse?> GetTokenAsync(string code)
        {
            var queries = HttpUtility.ParseQueryString(string.Empty);
            queries.Add("grant_type", "authorization_code");
            queries.Add("code", code);
            queries.Add("client_id", _options.Providers.Twitch.ClientId);
            queries.Add("client_secret", _options.Providers.Twitch.ClienbtSecret);
            queries.Add("redirect_uri", _options.Providers.Twitch.Callbacks.Bot);

            var result = await _httpClient.PostAsync(
                $"{_options.Providers.Twitch.Endpoints.Token}?{queries}",
                null);

            if (result.IsSuccessStatusCode)
            {
                _options.Providers.Twitch.State = string.Empty;
                return JsonConvert.DeserializeObject<TwitchResponse>(await result.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
