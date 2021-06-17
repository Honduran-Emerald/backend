using Emerald.Domain.Models.ChatAggregate;
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
    public class ChatMessageRepository : IChatMessageRepository
    {
        private IMongoCollection<ChatMessage> collection;
        private IMediator mediator;

        public ChatMessageRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<ChatMessage>("ChatMessages");
            collection.Indexes.CreateOneAsync(new CreateIndexModel<ChatMessage>(
                Builders<ChatMessage>.IndexKeys
                    .Ascending(c => c.ReceiverId)
                    .Ascending(c => c.SenderId)));

            this.mediator = mediator;
        }
        public async Task<ChatMessage> Get(ObjectId id)
        {
            var chatMessage = await collection.Find(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (chatMessage == null)
            {
                throw new MissingElementException($"Tracker '{id}' not found");
            }

            return chatMessage;
        }

        public async Task Add(ChatMessage chatMessage)
        {
            await collection.InsertOneAsync(chatMessage);
            await mediator.PublishEntity(chatMessage);
        }

        public async Task Update(ChatMessage chatMessage)
        {
            await collection.ReplaceOneAsync(o => o.Id == chatMessage.Id, chatMessage);
            await mediator.PublishEntity(chatMessage);
        }

        public async Task Remove(ChatMessage chatMessage)
        {
            await collection.DeleteOneAsync(c => c.Id == chatMessage.Id);
            await mediator.PublishEntity(chatMessage);
        }

        public async Task RemoveAll(List<ObjectId> components)
        {
            await collection.DeleteManyAsync(f => components.Contains(f.Id));
        }

        public IMongoQueryable<ChatMessage> GetQueryable()
        {
            return collection.AsQueryable();
        }
    }
}
