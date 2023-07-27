namespace Soncoord.Infrastructure.Auth
{
    public class RevokeResponse : IRevokeResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
