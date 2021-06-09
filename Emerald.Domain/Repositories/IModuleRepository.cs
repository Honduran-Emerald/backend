using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Domain.Repositories
{
    public interface IModuleRepository
    {
        Task Add(Module module);
        Task Update(Module module);
        Task Remove(Module module);

        Task<Module> Get(ObjectId id);
        Task<List<Module>> GetForQuest(QuestVersion quest);
        IMongoQueryable<Module> GetQueryable();
    }
}
