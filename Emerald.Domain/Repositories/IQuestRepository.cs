using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IQuestRepository
    {
        Task Remove(Quest quest);
        Task Add(Quest quest);
        Task Update(Quest quest);

        Task<Quest> Get(ObjectId questId);
        IMongoQueryable<Quest> GetQueryable();

        IMongoCollection<Quest> Collection { get; }
    }
}
