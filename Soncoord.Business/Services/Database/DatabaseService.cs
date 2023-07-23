using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Configuration;

namespace Soncoord.Business.Services.Database
{
    public class Test
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("channels")]
        public List<string> Channels { get; set; }
    }

    public class DatabaseService //: IDatabaseService
    {
        private readonly AppSettings _options;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public DatabaseService(IOptions<AppSettings> options)
        {
            _options = options.Value;

            _client = new MongoClient(options.Value.DbConnectionString);
            _database = _client.GetDatabase("soncoord");
        }

        public async Task<List<Test>> GetData()
        {
            return await _database.GetCollection<Test>("test").Find(Builders<Test>.Filter.Empty).ToListAsync();
        }
    }
}
