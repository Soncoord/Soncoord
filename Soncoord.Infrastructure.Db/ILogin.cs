using MongoDB.Bson;

namespace Soncoord.Infrastructure.Db
{
    public interface ILogin
    {
        ObjectId Id { get; set; }
        DateTime DateTime { get; set; }
        string State { get; set; }
    }
}
