using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IQuestRepository
    {
        Task Add(Quest quest);
        Task<Quest> Get(ObjectId questId);
        Task Update(Quest quest);
    }
}
