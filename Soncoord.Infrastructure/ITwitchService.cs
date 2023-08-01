using Soncoord.Infrastructure.Auth;

namespace Soncoord.Infrastructure
{
    public interface ITwitchService
    {
        Task<string> Authorize();
        Task<IAuthResponse?> GetTokenAsync(string code);
        Task<IAuthResponse?> RefreshTokenAsync(string refreshToken);
        Task<IValidateResponse?> ValidateTokenAsync(string? token);
        Task<IRevokeResponse?> RevokeTokenAsync(string? token);
    }
}
