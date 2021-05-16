using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class QuestRepository : IQuestRepository
    {
        private IMongoCollection<Quest> collection;

        public QuestRepository(IMongoDbContext dbContext)
        {
            collection = dbContext.Emerald.GetCollection<Quest>("Quests");
        }

        public async Task Add(Quest quest)
        {
            await collection.InsertOneAsync(quest);
        }

        public async Task<Quest> Get(ObjectId questId)
        {
            return await collection.Find(q => q.Id == questId)
                .FirstAsync();
        }

        public async Task Update(Quest quest)
        {
            await collection.ReplaceOneAsync(q => q.Id == quest.Id, quest);
        }
    }
}
