using Emerald.Domain.Models.QuestAggregate;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class QuestRepository : IQuestRepository
    {
        private IMongoCollection<Quest> collection;
        private Mediator mediator;

        public QuestRepository(IMongoDbContext dbContext, Mediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Quest>("Quests");
            this.mediator = mediator;
        }

        public async Task Add(Quest quest)
        {
            await mediator.PublishEntity(quest);
            await collection.InsertOneAsync(quest);
        }

        public async Task<Quest> Get(ObjectId questId)
        {
            return await collection.Find(q => q.Id == questId)
                .FirstAsync();
        }

        public IMongoQueryable<Quest> GetQueryable()
        {
            return collection.AsQueryable();
        }

        public async Task Update(Quest quest)
        {
            await mediator.PublishEntity(quest);
            await collection.ReplaceOneAsync(q => q.Id == quest.Id, quest);
        }
    }
}
