using Emerald.Domain.Models.PrototypeAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Repositories
{
    public interface IQuestPrototypeRepository
    {
        Task<QuestPrototype> Get(ObjectId id);
        Task Update(QuestPrototype questPrototype);
        Task Add(QuestPrototype questPrototype);
    }
}
