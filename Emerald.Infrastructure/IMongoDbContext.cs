using MongoDB.Driver;

namespace Emerald.Infrastructure
{
    public interface IMongoDbContext
    {
        IMongoDatabase Emerald { get; }
    }
}
