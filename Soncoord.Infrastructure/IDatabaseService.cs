using MongoDB.Driver;
using Soncoord.Infrastructure.Database;

namespace Soncoord.Infrastructure
{
    public interface IDatabaseService
    {
        Task<List<Test>> GetData();
    }
}
