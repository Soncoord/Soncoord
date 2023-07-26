using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Auth;
using Soncoord.Infrastructure.Configuration;
using System.Net.Http.Headers;
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
            // Create Guid and save it
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

        public async Task<IAuthResponse?> GetTokenAsync(string code)
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
                //ToDo Remove State info
                _options.Providers.Twitch.State = string.Empty;
                return JsonConvert.DeserializeObject<AuthResponse>(await result.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<IValidateResponse?> ValidateAsync(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                //token = Get Token
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await _httpClient.GetAsync($"{_options.Providers.Twitch.Endpoints.Validate}");
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ValidateResponse>(await result.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
