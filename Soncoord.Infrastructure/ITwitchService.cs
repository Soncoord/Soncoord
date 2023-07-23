using Soncoord.Infrastructure.Auth;

namespace Soncoord.Infrastructure
{
    public interface ITwitchService
    {
        string Authorize();
        Task<TwitchResponse?> GetTokenAsync(string code);
    }
}
