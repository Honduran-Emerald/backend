using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private IMongoCollection<Module> collection;

        public ModuleRepository(IMongoDbContext dbContext)
        {
            collection = dbContext.Emerald.GetCollection<Module>("Modules");
        }

        public async Task Add(Module module)
        {
            await collection.InsertOneAsync(module);
        }

        public async Task<Module> Get(ObjectId id)
        {
            return await collection.Find(o => o.Id == id)
                .FirstAsync();
        }

        public async Task Update(Module module)
        {
            await collection.ReplaceOneAsync(o => o.Id == module.Id, module);
        }
    }
}
