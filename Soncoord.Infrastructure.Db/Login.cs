using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Soncoord.Infrastructure.Db
{
    public class Login : ILogin
    {
        public ObjectId Id { get; set; }

        [BsonElement("date_time")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [BsonElement("state")]
        public string State { get; set; } = string.Empty;
    }
}
