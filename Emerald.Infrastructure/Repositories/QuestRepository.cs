     using Emerald.Domain.Models.QuestAggregate;
using Emerald.Infrastructure.Exceptions;
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
        private IMediator mediator;

        public QuestRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Quest>("Quests");
            this.mediator = mediator;
        }

        public async Task Add(Quest quest)
        {
            await collection.InsertOneAsync(quest);
            await mediator.PublishEntity(quest);
        }

        public async Task Update(Quest quest)
        {
            await collection.ReplaceOneAsync(q => q.Id == quest.Id, quest);
            await mediator.PublishEntity(quest);
        }

        public async Task<Quest> Get(ObjectId questId)
        {
            var quest = await collection.Find(q => q.Id == questId)
                .FirstOrDefaultAsync();

            if (quest == null)
            {
                throw new MissingElementException($"Quest '{questId}' not found");
            }

            return quest;
        }

        public IMongoQueryable<Quest> GetQueryable()
        {
            return collection.AsQueryable();
        }
    }
}
