using Emerald.Domain.Models.ComponentAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IComponentRepository
    {
        Task<Component> Get(ObjectId id);
        Task<IEnumerable<Component>> GetAll(List<ObjectId> ids);
        Task Add(Component component);
    }
}
