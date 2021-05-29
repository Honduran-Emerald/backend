using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
                .FirstOrDefaultAsync();

            if (tracker == null)
            {
                throw new MissingElementException($"Tracker '{id}' not found");
            }

            return tracker;
        }

        public async Task<bool> HasAnyTrackerForQuest(Quest quest)
        {
            return await collection
                .Find(t => t.QuestId == quest.Id)
                .AnyAsync();
        }

        public IMongoQueryable<Tracker> GetQueryable()
        {
            return collection.AsQueryable();
        }
    }
}
