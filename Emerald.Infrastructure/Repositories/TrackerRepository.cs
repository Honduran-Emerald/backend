using Emerald.Domain.Models.TrackerAggregate;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class TrackerRepository : ITrackerRepository
    {
        private IMongoCollection<Tracker> collection;
        private Mediator mediator;

        public TrackerRepository(IMongoDbContext dbContext, Mediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Tracker>("Trackers");
            this.mediator = mediator;
        }

        public async Task Add(Tracker tracker)
        {
            await mediator.PublishEntity(tracker);
            await collection.InsertOneAsync(tracker);
        }

        public async Task<Tracker> Get(ObjectId id)
        {
            return await collection.Find(o => o.Id == id)
                .FirstAsync();
        }

        public async Task Update(Tracker tracker)
        {
            await mediator.PublishEntity(tracker);
            await collection.ReplaceOneAsync(o => o.Id == tracker.Id, tracker);
        }
    }
}
