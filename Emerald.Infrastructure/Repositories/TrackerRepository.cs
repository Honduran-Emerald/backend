using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class TrackerRepository : ITrackerRepository
    {
        private IMongoCollection<Tracker> collection;
        private IMediator mediator;

        public TrackerRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Tracker>("Trackers");
            this.mediator = mediator;
        }

        public async Task Add(Tracker tracker)
        {
            await collection.InsertOneAsync(tracker);
            await mediator.PublishEntity(tracker);
        }

        public async Task Update(Tracker tracker)
        {
            await collection.ReplaceOneAsync(o => o.Id == tracker.Id, tracker);
            await mediator.PublishEntity(tracker);
        }

        public async Task<Tracker> Get(ObjectId id)
        {
            var tracker = await collection.Find(o => o.Id == id)
                .FirstAsync();

            if (tracker == null)
            {
                throw new MissingElementException();
            }

            return tracker;
        }
    }
}
