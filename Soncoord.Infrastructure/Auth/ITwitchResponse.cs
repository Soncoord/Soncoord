namespace Soncoord.Infrastructure.Auth
{
    public interface ITwitchResponse
    {
        string AccessToken { get; set; }
        int ExpiresIn { get; set; }
        string RefreshToken { get; set; }
        string TokenType { get; set; }
        string[] Scope { get; set; }
    }
}
