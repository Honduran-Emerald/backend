using Emerald.Domain.Models.ChatAggregate;
using Emerald.Domain.Repositories;
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
    public class ChatRepository : IChatRepository
    {
        private IMongoCollection<Chat> collection;
        private IMediator mediator;

        public ChatRepository(IMongoDbContext dbContext, IMediator mediator)
        {
            collection = dbContext.Emerald.GetCollection<Chat>("Chats");
            collection.Indexes.CreateOneAsync(new CreateIndexModel<Chat>(
                Builders<Chat>.IndexKeys
                    .Ascending(c => c.UserReceiverId)
                    .Ascending(c => c.UserSenderId)));

            this.mediator = mediator;
        }

        public async Task<Chat> EmplaceGet(ObjectId userSenderId, ObjectId userReceiverId)
        {
            Chat chat = await collection.AsQueryable()
                .Where(c => c.UserSenderId == userSenderId && c.UserReceiverId == userReceiverId)
                .FirstOrDefaultAsync();

            if (chat == null)
            {
                chat = new Chat(userSenderId, userReceiverId);
                await collection.InsertOneAsync(chat);
            }

            return chat;
        }

        public IMongoQueryable<Chat> GetQueryable()
        {
            return collection.AsQueryable();
        }

        public async Task Update(Chat chat)
        {
            await collection.ReplaceOneAsync(c => c.Id == chat.Id, chat);
            await mediator.PublishEntity(chat);
        }
    }
}
