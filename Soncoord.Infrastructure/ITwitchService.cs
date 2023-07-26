using Soncoord.Infrastructure.Auth;

namespace Soncoord.Infrastructure
{
    public interface ITwitchService
    {
        Task<string> Authorize();
        Task<IAuthResponse?> GetTokenAsync(string code);
        Task<IValidateResponse?> ValidateAsync(string? token);
    }
}
