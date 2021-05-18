using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Domain.Repositories
{
    public interface IModuleRepository
    {
        Task Add(Module module);
        Task<Module> Get(ObjectId id);
        Task<List<Module>> GetForQuest(QuestVersion quest);
        Task Update(Module module);
    }
}
