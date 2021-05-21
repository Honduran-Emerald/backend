﻿using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IQuestRepository
    {
        Task Add(Quest quest);
        Task Update(Quest quest);

        Task<Quest> Get(ObjectId questId);
        IMongoQueryable<Quest> GetQueryable();
    }
}
