using Emerald.Domain.Models.PrototypeAggregate;
using MongoDB.Bson;
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
