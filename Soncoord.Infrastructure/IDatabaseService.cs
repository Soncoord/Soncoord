using MongoDB.Bson;
using MongoDB.Driver;

namespace Soncoord.Infrastructure
{
    public interface IDatabaseService
    {
        IMongoCollection<BsonDocument> GetData();
    }
}
