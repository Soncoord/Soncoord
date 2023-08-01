using MongoDB.Bson;

namespace Soncoord.Infrastructure.Database
{
    public interface IBots
    {
        ObjectId Id { get; set; }
        string Name { get; set; }
        string UserId { get; set; }
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
        List<string>? Channels { get; set; }
    }
}
