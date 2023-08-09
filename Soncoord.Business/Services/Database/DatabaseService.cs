using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Configuration;
using Soncoord.Infrastructure.Db;

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

        public async Task<Bots> GetBotDataAsync()
        {
            return await _database
                .GetCollection<Bots>("bots")
                .Find(Builders<Bots>.Filter.Empty)
                .FirstOrDefaultAsync();
        }

        public async Task SaveBotDataAsync(Bots bot)
        {
            var count = await _database
                .GetCollection<Bots>("bots")
                .CountDocumentsAsync(Builders<Bots>.Filter.Eq(x => x.UserId, bot.UserId));

            if (count == 0)
            {
                await _database.GetCollection<Bots>("bots").InsertOneAsync(bot);
            }
            else
            {
                var filter = Builders<Bots>.Filter.Eq(x => x.UserId, bot.UserId);
                var update = Builders<Bots>.Update
                    .Set(x => x.Name, bot.Name)
                    .Set(x => x.UserId, bot.UserId)
                    .Set(x => x.AccessToken, bot.AccessToken)
                    .Set(x => x.RefreshToken, bot.RefreshToken);

                await _database.GetCollection<Bots>("bots").UpdateOneAsync(filter, update);
            }
        }
    }
}
