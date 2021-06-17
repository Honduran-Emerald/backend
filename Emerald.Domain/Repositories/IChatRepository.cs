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
    public interface IChatRepository
    {
        Task<Chat> EmplaceGet(ObjectId userSenderId, ObjectId userReceiverId);
        Task Update(Chat chat);
        IMongoQueryable<Chat> GetQueryable();
    }
}
