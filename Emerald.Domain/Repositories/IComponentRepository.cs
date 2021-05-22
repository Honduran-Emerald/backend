using Emerald.Domain.Models.ComponentAggregate;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IComponentRepository
    {
        Task Add(Component component);
        Task Update(Component component);
        Task Remove(Component component);
        Task RemoveAll(List<ObjectId> components);

        Task<Component> Get(ObjectId id);
        Task<IEnumerable<Component>> GetAll(List<ObjectId> ids);
        Task<IEnumerable<Component>> GetAll();
        IMongoQueryable<Component> GetQueryable();
    }
}
