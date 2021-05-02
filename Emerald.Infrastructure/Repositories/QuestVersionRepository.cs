using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class QuestVersionRepository : IQuestVersionRepository
    {
        private IMongoCollection<QuestVersion> collection;

        public QuestVersionRepository(IMongoDbContext dbContext)
        {
            collection = dbContext.Emerald.GetCollection<QuestVersion>("QuestVersions");
        }

        public async Task Add(QuestVersion questVersion)
        {
            await collection.InsertOneAsync(questVersion);
        }

        public async Task<QuestVersion> Get(ObjectId questVersionId)
        {
            return await collection.Find(q => q.Id == questVersionId)
                .FirstOrDefaultAsync();
        }

        public async Task Update(QuestVersion questVersion)
        {
            await collection.ReplaceOneAsync(q => q.Id == questVersion.Id, questVersion);
        }
    }
}
