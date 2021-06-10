using Emerald.Domain.Models.LockAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class LockRepository : ILockRepository
    {
        private IMongoCollection<Lock> collection;
        private IMediator mediator;

        public LockRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Lock>("Locks");
            this.mediator = mediator;
        }

        public async Task Add(Lock @lock)
        {
            await collection.InsertOneAsync(@lock);
            await mediator.PublishEntity(@lock);
        }

        public async Task Update(Lock @lock)
        {
            await collection.ReplaceOneAsync(o => o.Id == @lock.Id, @lock);
            await mediator.PublishEntity(@lock);
        }

        public async Task<Lock> Get(ObjectId id)
        {
            var @lock = await collection.Find(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (@lock == null)
            {
                throw new MissingElementException($"Lock '{id}' not found");
            }

            return @lock;
        }


        public async Task Remove(ObjectId id)
        {
            await collection.DeleteOneAsync(m => m.Id == id);
        }

        public IMongoQueryable<Lock> GetQueryable()
        {
            return collection.AsQueryable();
        }
    }
}
