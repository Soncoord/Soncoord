using MongoDB.Driver;
using Soncoord.Infrastructure.Database;

namespace Soncoord.Infrastructure
{
    public interface IDatabaseService
    {
        Task<List<Test>> GetData();
        Task AddLoginAsync(Login login);
        Task<Login> GetLoginAsync(string state);
        Task<DeleteResult> DeleteLoginAsync(string state);
    }
}
