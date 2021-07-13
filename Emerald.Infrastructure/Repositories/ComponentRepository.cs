using AspNetCore.Identity.Mongo.Mongo;
using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Infrastructure.Exceptions;
using Emerald.Infrastructure.Services;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private IMongoCollection<Component> collection;
        private IMediator mediator;
        private IImageIndexService imageIndexService;

        public ComponentRepository(IMongoDbContext dbContext, IMediator mediator, IImageIndexService imageIndexService)
        {
            collection = dbContext.Emerald.GetCollection<Component>("Components");
            this.mediator = mediator;
            this.imageIndexService = imageIndexService;
        }

        public async Task<Component> Get(ObjectId id)
        {
            var component = await collection
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (component == null)
            {
                throw new MissingElementException($"Component '{id}' not found");
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

            if (component is ImageComponent imageComponent)
            {
                await imageIndexService.IncreaseImageReference(imageComponent.ImageId!);
            }
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

        public async Task Remove(Component component)
        {
            await collection.DeleteOneAsync(c => c.Id == component.Id);
            await mediator.PublishEntity(component);

            if (component is ImageComponent imageComponent)
            {
                await imageIndexService.DecreaseImageReference(imageComponent.ImageId!);
            }
        }

        public async Task RemoveAll(List<ObjectId> components)
        {
            await collection.DeleteManyAsync(f => components.Contains(f.Id));
        }

        public IMongoQueryable<Component> GetQueryable()
        {
            return collection.AsQueryable();
        }
    }
}
