using Emerald.Domain.Models.ChatAggregate;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Repositories
{
    public interface IChatMessageRepository
    {
        Task<ChatMessage> Get(ObjectId id);
        Task Add(ChatMessage message);
        Task Remove(ChatMessage message);

        IMongoQueryable<ChatMessage> GetQueryable();
    }
}
