using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class TrackerRepository : ITrackerRepository
    {
        private IMongoCollection<Tracker> collection;

        public TrackerRepository(IMongoDbContext dbContext)
        {
            collection = dbContext.Emerald.GetCollection<Tracker>("Trackers");
        }

        public async Task Add(Tracker tracker)
        {
            await collection.InsertOneAsync(tracker);
        }

        public async Task<Tracker> Get(ObjectId id)
        {
            return await collection.Find(o => o.Id == id)
                .FirstAsync();
        }

        public async Task Update(Tracker tracker)
        {
            await collection.ReplaceOneAsync(o => o.Id == tracker.Id, tracker);
        }
    }
}
