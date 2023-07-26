using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Soncoord.Infrastructure.Database
{
    public class Test
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("channels")]
        public List<string> Channels { get; set; }
    }
}
