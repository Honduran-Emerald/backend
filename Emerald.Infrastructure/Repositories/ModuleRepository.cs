using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Repositories;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private IMongoCollection<Module> collection;
        private Mediator mediator;

        public ModuleRepository(IMongoDbContext dbContext, Mediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Module>("Modules");
            this.mediator = mediator;
        }

        public async Task Add(Module module)
        {
            await mediator.PublishEntity(module);
            await collection.InsertOneAsync(module);
        }

        public async Task<Module> Get(ObjectId id)
        {
            return await collection.Find(o => o.Id == id)
                .FirstAsync();
        }

        public async Task<List<Module>> GetForQuest(QuestVersion questVersion)
        {
            return await collection
                .Find(o => questVersion.ModuleIds.Contains(o.Id))
                .ToListAsync();
        }

        public async Task Update(Module module)
        {
            await mediator.PublishEntity(module);
            await collection.ReplaceOneAsync(o => o.Id == module.Id, module);
        }
    }
}
