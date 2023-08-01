namespace Soncoord.Infrastructure.Auth
{
    public interface IRevokeResponse
    {
        int Status { get; set; }
        string Message { get; set; }
    }
}
