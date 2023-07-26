namespace Soncoord.Infrastructure.Auth
{
    public interface IAuthResponse
    {
        string AccessToken { get; set; }
        int ExpiresIn { get; set; }
        string RefreshToken { get; set; }
        string TokenType { get; set; }
        string[] Scope { get; set; }
    }
}
