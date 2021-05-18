using AspNetCore.Identity.Mongo.Mongo;
using Emerald.Domain.Models.ComponentAggregate;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private IMongoCollection<Component> collection;
        private Mediator mediator;

        public ComponentRepository(IMongoDbContext dbContext, Mediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Component>("Components");
            this.mediator = mediator;
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
            await mediator.PublishEntity(component);
            await collection.InsertOneAsync(component);
        }

        public async Task<IEnumerable<Component>> GetAll()
        {
            return await collection
                .AsQueryable()
                .ToListAsync();
        }
    }
}
