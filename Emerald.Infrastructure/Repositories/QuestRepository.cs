using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Infrastructure.ElasticModels;
using Emerald.Infrastructure.Exceptions;
using Emerald.Infrastructure.Services;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Nest;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class QuestRepository : IQuestRepository
    {
        private IMongoCollection<Quest> collection;
        private IMediator mediator;
        private IImageIndexService imageIndexService;
        private IElasticService elasticSearchService;
        private QuestElasticModelFactory questElasticModelFactory;

        public QuestRepository(IMongoDbContext dbContext, IMediator mediator, IImageIndexService imageIndexService, IElasticService elasticSearchService, QuestElasticModelFactory questElasticModelFactory)
        {
            collection = dbContext.Emerald.GetCollection<Quest>("Quests");
            this.mediator = mediator;
            this.imageIndexService = imageIndexService;
            this.elasticSearchService = elasticSearchService;
            this.questElasticModelFactory = questElasticModelFactory;
        }

        public async Task Remove(Quest quest)
        {
            // await elasticSearchService.ElasticClient.DeleteAsync(DocumentPath<Quest>.Id(quest));
            if (quest.QuestVersions.Count > 0)
            {
                await elasticSearchService.ElasticClient.DeleteByQueryAsync<QuestElasticModel>(
                    q => q.Query(rq => rq.Match(
                        m => m.Field(f => f.Id).Query(quest.Id.ToString())
                    )));
            }

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
            if (quest.QuestVersions.Count > 0)
            {
                await elasticSearchService.ElasticClient.IndexDocumentOrThrow(
                await questElasticModelFactory.Create(quest));
            }
            await collection.InsertOneAsync(quest);
            await mediator.PublishEntity(quest);
        }

        public async Task Update(Quest quest)
        {
            if (quest.QuestVersions.Count > 0)
            {
                await elasticSearchService.ElasticClient.IndexDocumentOrThrow(
                    await questElasticModelFactory.Create(quest));
            }
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
