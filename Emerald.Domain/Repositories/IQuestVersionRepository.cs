using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IQuestVersionRepository
    {
        Task Add(QuestVersion quest);
        Task<QuestVersion> Get(ObjectId questId);
        Task Update(QuestVersion quest);
    }
}
