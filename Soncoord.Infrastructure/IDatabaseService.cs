using MongoDB.Driver;
using Soncoord.Infrastructure.Database;

namespace Soncoord.Infrastructure
{
    public interface IDatabaseService
    {
        Task AddLoginAsync(Login login);
        Task<Login> GetLoginAsync(string state);
        Task<DeleteResult> DeleteLoginAsync(string state);
        Task<Bots> GetBotDataAsync();
        Task SaveBotDataAsync(Bots bot);
    }
}
