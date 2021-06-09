using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private IMongoCollection<Module> collection;
        private IMediator mediator;

        public ModuleRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Module>("Modules");
            this.mediator = mediator;
        }

        public async Task Add(Module module)
        {
            await collection.InsertOneAsync(module);
            await mediator.PublishEntity(module);
        }

        public async Task Update(Module module)
        {
            await collection.ReplaceOneAsync(o => o.Id == module.Id, module);
            await mediator.PublishEntity(module);
        }

        public async Task<Module> Get(ObjectId id)
        {
            var module = await collection.Find(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (module == null)
            {
                throw new MissingElementException($"Module '{id}' not found");
            }

            return module;
        }

        public async Task<List<Module>> GetForQuest(QuestVersion questVersion)
        {
            if (questVersion.ModuleIds == null)
            {
                return new List<Module>();
            }

            var modules = await collection
                .Find(o => questVersion.ModuleIds.Contains(o.Id))
                .ToListAsync();

            if (modules.Count != questVersion.ModuleIds.Count)
            {
                throw new MissingElementException();
            }

            return modules;
        }

        public async Task Remove(Module module)
        {
            await collection.DeleteOneAsync(m => m.Id == module.Id);
        }

        public IMongoQueryable<Module> GetQueryable()
        {
            return collection.AsQueryable();
        }
    }
}
