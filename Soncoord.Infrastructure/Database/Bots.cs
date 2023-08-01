using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Soncoord.Infrastructure.Database
{
    public class Bots : IBots
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [BsonElement("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [BsonElement("channels")]
        public List<string>? Channels { get; set; }
    }
}
