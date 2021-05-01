using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo.Mongo;
using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Emerald.Infrastructure.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private IMongoCollection<Component> collection;

        public ComponentRepository(MongoDbContext dbContext)
        {
            collection = dbContext.Emerald.GetCollection<Component>("Components");
        }

        public async Task<Component> Get(ObjectId id)
        {
            return await collection
                .Find(c => c.Id == id)
                .FirstAsync();
        }

        public async Task<IEnumerable<Component>> GetAll(List<ObjectId> ids)
        {
            return await collection
                .WhereAsync(c => ids.Contains(c.Id));
        }

        public async Task Add(Component component)
        {
            await collection.InsertOneAsync(component);
        }
    }
}
