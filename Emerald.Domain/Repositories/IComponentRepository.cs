using Emerald.Domain.Models.ComponentAggregate;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IComponentRepository
    {
        Task Add(Component component);
        Task Update(Component component);

        Task<Component> Get(ObjectId id);
        Task<IEnumerable<Component>> GetAll(List<ObjectId> ids);
        Task<IEnumerable<Component>> GetAll();
    }
}
