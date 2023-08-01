namespace Soncoord.Infrastructure.Auth
{
    public interface IValidateResponse
    {
        string? ClientId { get; set; }
        string? Login { get; set; }
        string[]? Scopes { get; set; }
        string? UserId { get; set; }
        int? ExpiresIn { get; set; }
        string? Status { get; set; }
        string? Message { get; set; }
    }
}
