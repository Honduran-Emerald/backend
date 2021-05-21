using AspNetCore.Identity.Mongo.Mongo;
using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Infrastructure.Exceptions;
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
        private IMediator mediator;

        public ComponentRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Component>("Components");
            this.mediator = mediator;
        }

        public async Task<Component> Get(ObjectId id)
        {
            var component = await collection
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (component == null)
            {
                throw new MissingElementException();
            }

            return component;
        }

        public async Task<IEnumerable<Component>> GetAll(List<ObjectId> ids)
        {
            return await collection
                .WhereAsync(c => ids.Contains(c.Id));
        }

        public async Task Add(Component component)
        {
            await collection.InsertOneAsync(component);
            await mediator.PublishEntity(component);
        }

        public async Task<IEnumerable<Component>> GetAll()
        {
            return await collection
                .AsQueryable()
                .ToListAsync();
        }

        public async Task Update(Component component)
        {
            await collection.ReplaceOneAsync(o => o.Id == component.Id, component);
            await mediator.PublishEntity(component);
        }
    }
}
