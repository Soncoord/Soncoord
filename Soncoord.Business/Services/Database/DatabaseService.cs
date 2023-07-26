using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Configuration;
using Soncoord.Infrastructure.Database;

namespace Soncoord.Business.Services.Database
{
    internal class DatabaseService : IDatabaseService
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
            return await _database
                .GetCollection<Test>("test")
                .Find(Builders<Test>.Filter.Empty)
                .ToListAsync();
        }

        public async Task AddLoginAsync(Login login)
        {
            await _database
                .GetCollection<Login>("logins")
                .InsertOneAsync(login);
        }

        public async Task<Login> GetLoginAsync(string state)
        {
            return await _database
                .GetCollection<Login>("logins")
                .Find(Builders<Login>.Filter.Eq(x => x.State, state))
                .FirstOrDefaultAsync();
        }

        public async Task<DeleteResult> DeleteLoginAsync(string state)
        {
            return await _database
                .GetCollection<Login>("logins")
                .DeleteOneAsync(Builders<Login>.Filter.Eq(x => x.State, state));
        }
    }
}
