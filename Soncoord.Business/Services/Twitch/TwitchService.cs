using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Auth;
using Soncoord.Infrastructure.Configuration;
using Soncoord.Infrastructure.Database;
using System.Net.Http.Headers;
using System.Web;

namespace Soncoord.Business.Services.Twitch
{
    public class TwitchService : ITwitchService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _options;
        private readonly IDatabaseService _database;

        public TwitchService(
            HttpClient httpClient,
            IOptions<AppSettings> options,
            IDatabaseService database)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _database = database;
        }

        public async Task<string> Authorize()
        {
            var state = Guid.NewGuid().ToString();
            await _database.AddLoginAsync(new Login { State = state });

            var queries = HttpUtility.ParseQueryString(string.Empty);
            queries.Add("response_type", "code");
            // TODo: Add scopes for chat
            queries.Add("scope", "user:read:email");
            queries.Add("client_id", _options.Providers.Twitch.ClientId);
            queries.Add("client_secret", _options.Providers.Twitch.ClientSecret);
            queries.Add("redirect_uri", _options.Providers.Twitch.Callbacks.Bot);
            queries.Add("state", state);

            return $"{_options.Providers.Twitch.Endpoints.Authorize}?{queries}";
        }

        public async Task<IAuthResponse?> GetTokenAsync(string code)
        {
            var queries = HttpUtility.ParseQueryString(string.Empty);
            queries.Add("grant_type", "authorization_code");
            queries.Add("code", code);
            queries.Add("client_id", _options.Providers.Twitch.ClientId);
            queries.Add("client_secret", _options.Providers.Twitch.ClientSecret);
            queries.Add("redirect_uri", _options.Providers.Twitch.Callbacks.Bot);

            var result = await _httpClient.PostAsync(
                $"{_options.Providers.Twitch.Endpoints.Token}?{queries}",
                null);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AuthResponse>(await result.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<IAuthResponse?> RefreshTokenAsync(string refreshToken)
        {
            var queries = HttpUtility.ParseQueryString(string.Empty);
            queries.Add("grant_type", "refresh_token");
            queries.Add("refresh_token", HttpUtility.UrlEncode(refreshToken));
            queries.Add("client_id", _options.Providers.Twitch.ClientId);
            queries.Add("client_secret", _options.Providers.Twitch.ClientSecret);

            var result = await _httpClient.PostAsync(
                $"{_options.Providers.Twitch.Endpoints.Token}?{queries}",
                null);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AuthResponse>(await result.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<IValidateResponse?> ValidateTokenAsync(string? token)
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

        public async Task<IRevokeResponse?> RevokeTokenAsync(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                //token = Get Token
            }

            var queries = HttpUtility.ParseQueryString(string.Empty);
            queries.Add("client_id", _options.Providers.Twitch.ClientId);
            queries.Add("token", token);

            var result = await _httpClient.PostAsync(
                $"{_options.Providers.Twitch.Endpoints.Revoke}?{queries}",
                null);
            
            if(result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<RevokeResponse>(await result.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
