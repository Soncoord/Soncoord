using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Configuration;
using Soncoord.Infrastructure.Database;

namespace Soncoord.Business.Services.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public DatabaseService(IOptions<AppSettings> options)
        {
            _client = new MongoClient(options.Value.DbConnectionString);
            _database = _client.GetDatabase("soncoord");
        }

        public async Task<List<Test>> GetData()
        {
            return await _database.GetCollection<Test>("test").Find(Builders<Test>.Filter.Empty).ToListAsync();
        }
    }
}
