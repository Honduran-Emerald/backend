using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class QuestPrototypeRepository : IQuestPrototypeRepository
    {
        private IMongoCollection<QuestPrototype> collection;
        private IMediator mediator;

        public QuestPrototypeRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<QuestPrototype>("QuestPrototypes");
            this.mediator = mediator;
        }

        public async Task Add(QuestPrototype questPrototype)
        {
            await collection.InsertOneAsync(questPrototype);
            await mediator.PublishEntity(questPrototype);
        }

        public async Task<QuestPrototype> Get(ObjectId id)
        {
            var module = await collection.Find(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (module == null)
            {
                throw new MissingElementException($"QuestPrototype '{id}' not found");
            }

            return module;
        }

        public async Task Update(QuestPrototype questPrototype)
        {
            await collection.ReplaceOneAsync(f => f.Id == questPrototype.Id, questPrototype);
            await mediator.PublishEntity(questPrototype);
        }
    }
}
