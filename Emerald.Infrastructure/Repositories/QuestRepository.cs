using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Infrastructure.Exceptions;
using Emerald.Infrastructure.Services;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class QuestRepository : IQuestRepository
    {
        private IMongoCollection<Quest> collection;
        private IMediator mediator;
        private IImageIndexService imageIndexService;

        public QuestRepository(IMongoDbContext dbContext, IMediator mediator, IImageIndexService imageIndexService)
        {
            collection = dbContext.Emerald.GetCollection<Quest>("Quests");
            collection.Indexes.CreateOne(new CreateIndexModel<Quest>(Builders<Quest>.IndexKeys
                .Geo2DSphere(q => q.QuestVersions.Last().Location)));
            collection.Indexes.CreateOne(new CreateIndexModel<Quest>(Builders<Quest>.IndexKeys
                .Geo2DSphere(q => q.QuestVersions[0].Location)));
            this.mediator = mediator;
            this.imageIndexService = imageIndexService;
        }

        public async Task Remove(Quest quest)
        {
            await collection.DeleteOneAsync(q => q.Id == quest.Id);

            foreach (QuestVersion questVersion in quest.QuestVersions)
            {
                if (questVersion.ImageId != null)
                    await imageIndexService.DecreaseImageReference(questVersion.ImageId);

                if (questVersion.AgentProfileImageId != null)
                    await imageIndexService.DecreaseImageReference(questVersion.AgentProfileImageId);
            }
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

        public IMongoCollection<Quest> Collection => collection;
    }
}
