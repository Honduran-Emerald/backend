using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface ITrackerRepository
    {
        Task Add(Tracker tracker);
        Task Update(Tracker tracker);
        Task Remove(Tracker tracker);

        Task<bool> HasAnyTrackerForQuest(Quest quest);
        Task<Tracker> Get(ObjectId id);
        Task<Tracker?> FindByUserAndQuest(ObjectId userId, ObjectId questId);
        IMongoQueryable<Tracker> GetQueryable();
    }
}
