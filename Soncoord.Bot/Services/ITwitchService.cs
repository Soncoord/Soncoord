using Soncoord.Infrastructure.Auth;

namespace Soncoord.Bot.Services
{
    public interface ITwitchService
    {
        string Authorize();
        Task<TwitchResponse?> GetTokenAsync(string code);
    }
}
